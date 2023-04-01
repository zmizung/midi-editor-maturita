using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NAudio.Midi;

namespace MIDI_Editor
{
    public partial class Form1 : Form
    {
        public static string defaultPath = Directory.GetCurrentDirectory() + "/New MIDI file.mid";
        public static string selectedPath = Directory.GetCurrentDirectory() + "/backup.mid";

        public MidiFile mf = new MidiFile(defaultPath, true);

        public float pixPerTick = 0.5f;
        public float sph = 24;
        public int slotWidth = 16;
        public int xOffset = 0;
        public int yOffset = 40;

        string[] noteNames = new string[128];

        public bool noteSelected = false;
        int noteIndex = 0;
        int eventIndex = 0;

        public bool addingNote = false;

        public int nearestSlot;

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
                button11.Show();
                button13.Show();
                button14.Show();
                trackBar2.Show();
                trackBar2.Value = (((NoteOnEvent)mf.Events[0][eventIndex]).Velocity);
            }
            else
            {
                button4.Hide();
                button5.Hide();
                button6.Hide();
                button7.Hide();
                button8.Hide();
                button9.Hide();
                button11.Hide();
                button13.Hide();
                button14.Hide();
                trackBar2.Hide();
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
                    selectedPath = openFileDialog.FileName;
                    mf = new MidiFile(openFileDialog.FileName, true);
                    panel1.Refresh();
                    textBox1.Text = openFileDialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    selectedPath = Directory.GetCurrentDirectory() + "/backup.mid";
                }
            }

            if (mf.FileFormat == 1)
            {
                MessageBox.Show("Warning", "The selected MIDI file includes multiple tracks. The editor may not process it properly.", MessageBoxButtons.OK);
            }

            noteSelected = false;
            ShowNoteControls();
            panel1.Refresh();
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

            panel1.Cursor = Cursors.Default;
            addingNote = false;

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
                double pixPerSlot = Math.Floor(slotWidth * pixPerTick * mf.DeltaTicksPerQuarterNote / 16);

                for (int i = 0; i <= panel1.Width - 40; i++)
                {
                    e.Graphics.DrawLine(penG, 40 + (i * (int)pixPerSlot) - xOffset, 0, 40 + (i * (int)pixPerSlot) - xOffset, panel1.Height);
                }

                e.Graphics.DrawLine(penW, 40, 0, 40, panel1.Height);

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

            int index = 0;
            noteIndex = 0;
            eventIndex = 0;

            nearestSlot = (int)Math.Floor((double)selectedTime / mf.DeltaTicksPerQuarterNote * 16 / slotWidth);

            if (addingNote)
            {
                NoteOnEvent newNoteOn = new NoteOnEvent(nearestSlot * mf.DeltaTicksPerQuarterNote * slotWidth / 16, 1, selectedPitch, 100, slotWidth * mf.DeltaTicksPerQuarterNote / 16);
                NoteEvent newNoteOff = new NoteEvent(newNoteOn.AbsoluteTime + mf.DeltaTicksPerQuarterNote * slotWidth / 16, 1, MidiCommandCode.NoteOff, selectedPitch, 100);

                newNoteOn.OffEvent = newNoteOff;

                int onIndex = 0;
                int offIndex = 0;

                foreach (var midiEvent in mf.Events[0])
                {
                    if (midiEvent.AbsoluteTime == newNoteOn.AbsoluteTime && MidiEvent.IsNoteOn(midiEvent))
                    {
                        onIndex = index;
                        break;
                    }

                    else if (midiEvent.AbsoluteTime > newNoteOn.AbsoluteTime)
                    {
                        onIndex = index;
                        break;
                    }

                    else if (midiEvent.AbsoluteTime == newNoteOff.AbsoluteTime && MidiEvent.IsNoteOff(midiEvent))
                    {
                        offIndex = index;
                        break;
                    }

                    else if (midiEvent.AbsoluteTime > newNoteOff.AbsoluteTime)
                    {
                        offIndex = index;
                        break;
                    }

                    else
                    {
                        onIndex = index;
                        offIndex = index;
                    }

                    index++;
                }

                if (mf.Events[0][mf.Events[0].Count - 1].AbsoluteTime < newNoteOff.AbsoluteTime)
                {
                    mf.Events[0][mf.Events[0].Count - 1].AbsoluteTime = newNoteOff.AbsoluteTime;
                }

                mf.Events[0].Insert(onIndex, newNoteOn);
                mf.Events[0].Insert(offIndex + 1, newNoteOff);
                //využit zdroj https://stackoverflow.com/questions/10284952/c-sharp-way-to-add-value-in-a-listt-at-index
            }
            else
            {
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
            string notesInfo = "If the events are not properly sorted, try saving the file." + Environment.NewLine + Environment.NewLine;

            foreach (var midiEvent in mf.Events[0])
            {
                notesInfo += midiEvent.ToString() + Environment.NewLine;
            }

            MessageBox.Show(notesInfo);
            
            //postup převzat z https://social.msdn.microsoft.com/Forums/vstudio/en-US/25963105-d8c1-4e98-987d-4a970a185afd/how-to-show-all-text-of-a-string-array-in-a-messageboxshow?forum=csharpgeneral
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

            long offTime = ((NoteOnEvent)mf.Events[0][eventIndex]).AbsoluteTime + ((NoteOnEvent)mf.Events[0][eventIndex]).NoteLength;

            if (offTime >= mf.Events[0][mf.Events[0].Count - 1].AbsoluteTime)
            {
                mf.Events[0][mf.Events[0].Count - 1].AbsoluteTime = offTime;
            }

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

        private void button10_Click(object sender, EventArgs e)
        {
            panel1.Cursor = Cursors.Hand;
            addingNote = true;
            noteSelected = false;
            ShowNoteControls();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            mf.Events[0].RemoveAt(eventIndex);
            //využit zdroj https://stackoverflow.com/questions/10018957/how-to-remove-item-from-list-in-c
            noteSelected = false;
            ShowNoteControls();
            panel1.Refresh();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            MidiFile.Export(selectedPath, mf.Events);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (((NoteOnEvent)mf.Events[0][eventIndex]).NoteLength < mf.DeltaTicksPerQuarterNote * 64)
            {
                ((NoteOnEvent)mf.Events[0][eventIndex]).NoteLength += (int)(mf.DeltaTicksPerQuarterNote * slotWidth / 16);
            }

            long offTime = ((NoteOnEvent)mf.Events[0][eventIndex]).AbsoluteTime + ((NoteOnEvent)mf.Events[0][eventIndex]).NoteLength;

            if (offTime >= mf.Events[0][mf.Events[0].Count - 1].AbsoluteTime)
            {
                mf.Events[0][mf.Events[0].Count - 1].AbsoluteTime = offTime;
            }

            panel1.Refresh();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            {
                if (((NoteOnEvent)mf.Events[0][eventIndex]).NoteLength > mf.DeltaTicksPerQuarterNote * slotWidth / 16)
                {
                    ((NoteOnEvent)mf.Events[0][eventIndex]).NoteLength -= (int)(mf.DeltaTicksPerQuarterNote * slotWidth / 16);
                }

                panel1.Refresh();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to remove all notes?", "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
            // řádek převzat z https://social.msdn.microsoft.com/Forums/en-US/d9e89525-7133-41b7-8d30-0335d3e801f8/message-box-with-ok-and-cancel-buttons?forum=Vsexpressvcs
            {
                mf = new MidiFile(defaultPath, true);
                panel1.Refresh();
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            ((NoteOnEvent)mf.Events[0][eventIndex]).Velocity = trackBar2.Value;
            ((NoteOnEvent)mf.Events[0][eventIndex]).OffEvent.Velocity = trackBar2.Value;
        }
    }
}