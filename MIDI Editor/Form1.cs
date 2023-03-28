using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NAudio.Midi;

namespace MIDI_Editor
{
    public partial class Form1 : Form
    {
        public static string sDefaultPath = Directory.GetCurrentDirectory() + "/New MIDI file.mid";
        public static string sSelectedPath;

        public MidiFile mf = new MidiFile(sDefaultPath, true);

        public float pixPerTick = 0.5f;
        public float sph = 24;
        public int slotWidth = 16;
        public int xOffset = 0;
        public int yOffset = 40;

        string[] noteNames = new string[128];

        public bool noteSelected = false;
        int noteIndex = 0;
        int eventIndex = 0;

        public Form1()
        {
            InitializeComponent();

            hScrollBar1.Value = xOffset;
            vScrollBar1.Value = yOffset;
            trackBar1.Value = (int)(pixPerTick * 100);
            comboBox1.SelectedIndex = 4;

            for (int i = 0; i <= 127; i += 12)
            {
                noteNames[i] = $"C{i / 12}";
                noteNames[i + 1] = $"C#{i / 12}";
                noteNames[i + 2] = $"D{i / 12}";
                noteNames[i + 3] = $"D#{i / 12}";
                noteNames[i + 4] = $"E{i / 12}";
                noteNames[i + 5] = $"F{i / 12}";
                noteNames[i + 6] = $"F#{i / 12}";
                noteNames[i + 7] = $"G{i / 12}";

                if(i != 120)
                {
                    noteNames[i + 8] = $"G#{i / 12}";
                    noteNames[i + 9] = $"A{i / 12}";
                    noteNames[i + 10] = $"A#{i / 12}";
                    noteNames[i + 11] = $"B{i / 12}";
                }
            }
        }

        private void ShowNoteControls()
        {
            if (noteSelected)
            {
                button4.Show();
                button5.Show();
                button6.Show();
                button7.Show();
                button8.Show();
                button9.Show();
            }
            else
            {
                button4.Hide();
                button5.Hide();
                button6.Hide();
                button7.Hide();
                button8.Hide();
                button9.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)

        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "MIDI files (*.mid)|*.mid|All files (*.*)|*.*",
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    sSelectedPath = openFileDialog.FileName;
                    mf = new MidiFile(openFileDialog.FileName, true);
                    panel1.Refresh();
                    textBox1.Text = openFileDialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

            if (mf.FileFormat == 1)
            {
                MessageBox.Show("Warning", "The selected MIDI file includes multiple tracks. The editor may not process it properly.", 0);
            }

            noteSelected = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "MIDI files (*.mid)|*.mid|All files (*.*)|*.*",
                RestoreDirectory = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                MidiFile.Export(saveFileDialog.FileName, mf.Events);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);

            int index = 0;

            foreach (var midiEvent in mf.Events[0])
            {
                if (MidiEvent.IsNoteOn(midiEvent))
                {
                    NoteOnEvent noteOnEvent = (NoteOnEvent)midiEvent;

                    int x = (int)(noteOnEvent.AbsoluteTime * pixPerTick);
                    int y = (int)(panel1.Height / sph * (127 - noteOnEvent.NoteNumber - yOffset));
                    int width = (int)(noteOnEvent.NoteLength * pixPerTick);
                    int height = (int)(panel1.Height / sph);

                    if (noteSelected && index == noteIndex)
                    {
                        e.Graphics.FillRectangle(Brushes.Purple, x + 40 - xOffset, y, width, height);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(Brushes.Green, x + 40 - xOffset, y, width, height);
                    }

                    index++;
                }
            }

            using (Pen penW = new Pen(Color.White))
            using (Pen penG = new Pen(Color.Gray))
            {
                e.Graphics.DrawLine(penW, 40, 0, 40, panel1.Height);

                double pixPerSlot = Math.Floor(slotWidth * pixPerTick * mf.DeltaTicksPerQuarterNote / 16);

                for (int i = 0; i <= panel1.Width - 40; i++)
                {
                    e.Graphics.DrawLine(penG, 40 + (i * (int)pixPerSlot) - xOffset, 0, 40 + (i * (int)pixPerSlot) - xOffset, panel1.Height);
                }

                Font font = new Font(this.Font, FontStyle.Regular);

                e.Graphics.FillRectangle(Brushes.Black, 0, 0, 40, panel1.Height);

                for (int i = 0; i <= sph; i++)
                {
                    e.Graphics.DrawLine(penG, 40, 15 * i, panel1.Width, 15 * i);
                    e.Graphics.DrawLine(penW, 0, 15 * i, 40, 15 * i);

                    if (i + yOffset < noteNames.Length)
                    {
                        e.Graphics.DrawString(noteNames[127 - i - yOffset], font, Brushes.White, 5, 15 * i);
                    }
                }
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Point clickPoint = panel1.PointToClient(Cursor.Position);
            float selectedTime = (clickPoint.X - 40 + xOffset) / pixPerTick;
            int selectedSlot = (int)Math.Floor((double)clickPoint.Y / (panel1.Height / sph));
            int selectedPitch = 127 - yOffset - selectedSlot;
            string selectedNoteName = noteNames[selectedPitch];

            int index = 0;
            noteIndex = 0;
            eventIndex = 0;

            foreach (var midiEvent in mf.Events[0])
            {
                if (MidiEvent.IsNoteOn(midiEvent))
                {
                    NoteOnEvent noteOnEvent = (NoteOnEvent)midiEvent;

                    if (noteOnEvent.AbsoluteTime <= selectedTime && noteOnEvent.AbsoluteTime + noteOnEvent.NoteLength >= selectedTime && noteOnEvent.NoteNumber == selectedPitch)
                    {
                        noteSelected = true;
                        noteIndex = index;
                        ShowNoteControls();
                        panel1.Refresh();
                        break;
                    }

                    index++;
                    noteSelected = false;
                    ShowNoteControls();
                }
                eventIndex++;
            }
            panel1.Refresh();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pixPerTick = trackBar1.Value / 100f;
            panel1.Refresh();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            xOffset = hScrollBar1.Value;
            panel1.Refresh();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            yOffset = vScrollBar1.Value;
            panel1.Refresh();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {            
            slotWidth = 1 * (int)Math.Pow(2, comboBox1.SelectedIndex);
            panel1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var track in mf.Events)
            {
                foreach (var midiEvent in track)
                {
                    MessageBox.Show(midiEvent.ToString());
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (((NoteOnEvent)mf.Events[0][eventIndex]).NoteNumber != 127)
            {
                ((NoteOnEvent)mf.Events[0][eventIndex]).NoteNumber++;
                panel1.Refresh();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (((NoteOnEvent)mf.Events[0][eventIndex]).NoteNumber != 0)
            {
                ((NoteOnEvent)mf.Events[0][eventIndex]).NoteNumber--;
                panel1.Refresh();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (((NoteOnEvent)mf.Events[0][eventIndex]).NoteNumber <= 115)
            {
                ((NoteOnEvent)mf.Events[0][eventIndex]).NoteNumber+=12;
                panel1.Refresh();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (((NoteOnEvent)mf.Events[0][eventIndex]).NoteNumber >= 12)
            {
                ((NoteOnEvent)mf.Events[0][eventIndex]).NoteNumber-=12;
                panel1.Refresh();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int keepLength = ((NoteOnEvent)mf.Events[0][eventIndex]).NoteLength;

            if (((NoteOnEvent)mf.Events[0][eventIndex]).AbsoluteTime + (int)(mf.DeltaTicksPerQuarterNote * slotWidth / 16) < mf.DeltaTicksPerQuarterNote * 16 * 1024)
            {
                ((NoteOnEvent)mf.Events[0][eventIndex]).AbsoluteTime += (int)(mf.DeltaTicksPerQuarterNote * slotWidth / 16);
            }

            ((NoteOnEvent)mf.Events[0][eventIndex]).NoteLength = keepLength;
            panel1.Refresh();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int keepLength = ((NoteOnEvent)mf.Events[0][eventIndex]).NoteLength;

            if (((NoteOnEvent)mf.Events[0][eventIndex]).AbsoluteTime - (int)(mf.DeltaTicksPerQuarterNote * slotWidth / 16) >= 0)
            {
                ((NoteOnEvent)mf.Events[0][eventIndex]).AbsoluteTime -= (int)(mf.DeltaTicksPerQuarterNote * slotWidth / 16);
            }

            ((NoteOnEvent)mf.Events[0][eventIndex]).NoteLength = keepLength;

            panel1.Refresh();

        }
    }
}
