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

        readonly string[] noteNames = new string[128];

        public bool noteSelected = false;
        int noteIndex = 0;
        int eventIndex = 0;

        public bool addingNote = false;

        public int nearestSlot;

        public Form1()
        {
            InitializeComponent();

            ScrollBarX.Value = xOffset;
            ScrollBarY.Value = yOffset;
            ZoomX.Value = (int)(pixPerTick * 100);
            SlotBox.SelectedIndex = 4;

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
                NoteUp.Show();
                NoteDown.Show();
                OctUp.Show();
                OctDown.Show();
                NoteR.Show();
                NoteL.Show();
                NoteDel.Show();
                NoteExt.Show();
                NoteSho.Show();
                NoteVelo.Show();
                NoteVelo.Value = (((NoteOnEvent)mf.Events[0][eventIndex]).Velocity);
            }
            else
            {
                NoteUp.Hide();
                NoteDown.Hide();
                OctUp.Hide();
                OctDown.Hide();
                NoteR.Hide();
                NoteL.Hide();
                NoteDel.Hide();
                NoteExt.Hide();
                NoteSho.Hide();
                NoteVelo.Hide();
            }
        }

        private void OpenBtn_Click(object sender, EventArgs e)

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
                    mf = new MidiFile(openFileDialog.FileName, true);
                    selectedPath = openFileDialog.FileName;
                    GridPanel.Refresh();
                    FileNameBox.Text = openFileDialog.FileName;
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Unable to read file from disk. Original error: " + ex.Message);
                    selectedPath = Directory.GetCurrentDirectory() + "/backup.mid";
                }
            }

            if (mf.FileFormat == 1)
            {
                MessageBox.Show("Warning", "The selected MIDI file includes multiple tracks. The editor may not process it properly.", MessageBoxButtons.OK);
            }

            noteSelected = false;
            ShowNoteControls();
            GridPanel.Refresh();
        }

        private void SaveAsBtn_Click(object sender, EventArgs e)
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

        private void GridPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);

            GridPanel.Cursor = Cursors.Default;
            addingNote = false;

            int index = 0;

            foreach (var midiEvent in mf.Events[0])
            {
                if (MidiEvent.IsNoteOn(midiEvent))
                {
                    NoteOnEvent noteOnEvent = (NoteOnEvent)midiEvent;

                    int x = (int)(noteOnEvent.AbsoluteTime * pixPerTick);
                    int y = (int)(GridPanel.Height / sph * (127 - noteOnEvent.NoteNumber - yOffset));
                    int width = (int)(noteOnEvent.NoteLength * pixPerTick);
                    int height = (int)(GridPanel.Height / sph);

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

                for (int i = 0; i <= GridPanel.Width - 40; i++)
                {
                    e.Graphics.DrawLine(penG, 40 + (i * (int)pixPerSlot) - xOffset, 0, 40 + (i * (int)pixPerSlot) - xOffset, GridPanel.Height);
                }

                e.Graphics.DrawLine(penW, 40, 0, 40, GridPanel.Height);

                Font font = new Font(this.Font, FontStyle.Regular);

                e.Graphics.FillRectangle(Brushes.Black, 0, 0, 40, GridPanel.Height);

                for (int i = 0; i <= sph; i++)
                {
                    e.Graphics.DrawLine(penG, 40, 15 * i, GridPanel.Width, 15 * i);
                    e.Graphics.DrawLine(penW, 0, 15 * i, 40, 15 * i);

                    if (i + yOffset < noteNames.Length)
                    {
                        e.Graphics.DrawString(noteNames[127 - i - yOffset], font, Brushes.White, 5, 15 * i);
                    }
                }
            }
        }

        private void GridPanel_Click(object sender, EventArgs e)
        {
            Point clickPoint = GridPanel.PointToClient(Cursor.Position);
            float selectedTime = (clickPoint.X - 40 + xOffset) / pixPerTick;
            int selectedSlot = (int)Math.Floor((double)clickPoint.Y / (GridPanel.Height / sph));
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
                            GridPanel.Refresh();
                            break;
                        }

                        index++;
                        noteSelected = false;
                        ShowNoteControls();
                    }
                    eventIndex++;
                }
            }
            GridPanel.Refresh();
        }

        private void ZoomX_Scroll(object sender, EventArgs e)
        {
            pixPerTick = ZoomX.Value / 100f;
            GridPanel.Refresh();
        }

        private void ScrollBarX_Scroll(object sender, ScrollEventArgs e)
        {
            xOffset = ScrollBarX.Value;
            GridPanel.Refresh();
        }

        private void ScrollBarY_Scroll(object sender, ScrollEventArgs e)
        {
            yOffset = ScrollBarY.Value;
            GridPanel.Refresh();
        }

        private void SlotBox_SelectedIndexChanged(object sender, EventArgs e)
        {            
            slotWidth = 1 * (int)Math.Pow(2, SlotBox.SelectedIndex);
            GridPanel.Refresh();
        }

        private void InfoBtn_Click(object sender, EventArgs e)
        {
            string notesInfo = "If the events are not properly sorted, try saving the file." + Environment.NewLine + Environment.NewLine;

            foreach (var midiEvent in mf.Events[0])
            {
                notesInfo += midiEvent.ToString() + Environment.NewLine;
            }

            MessageBox.Show(notesInfo);
        }

        private void NoteUp_Click(object sender, EventArgs e)
        {
            if (((NoteOnEvent)mf.Events[0][eventIndex]).NoteNumber != 127)
            {
                ((NoteOnEvent)mf.Events[0][eventIndex]).NoteNumber++;
                GridPanel.Refresh();
            }
        }

        private void NoteDown_Click(object sender, EventArgs e)
        {
            if (((NoteOnEvent)mf.Events[0][eventIndex]).NoteNumber != 0)
            {
                ((NoteOnEvent)mf.Events[0][eventIndex]).NoteNumber--;
                GridPanel.Refresh();
            }
        }

        private void OctUp_Click(object sender, EventArgs e)
        {
            if (((NoteOnEvent)mf.Events[0][eventIndex]).NoteNumber <= 115)
            {
                ((NoteOnEvent)mf.Events[0][eventIndex]).NoteNumber+=12;
                GridPanel.Refresh();
            }
        }

        private void OctDown_Click(object sender, EventArgs e)
        {
            if (((NoteOnEvent)mf.Events[0][eventIndex]).NoteNumber >= 12)
            {
                ((NoteOnEvent)mf.Events[0][eventIndex]).NoteNumber-=12;
                GridPanel.Refresh();
            }
        }

        private void NoteR_Click(object sender, EventArgs e)
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

            GridPanel.Refresh();
        }

        private void NoteL_Click(object sender, EventArgs e)
        {
            int keepLength = ((NoteOnEvent)mf.Events[0][eventIndex]).NoteLength;

            if (((NoteOnEvent)mf.Events[0][eventIndex]).AbsoluteTime - (int)(mf.DeltaTicksPerQuarterNote * slotWidth / 16) >= 0)
            {
                ((NoteOnEvent)mf.Events[0][eventIndex]).AbsoluteTime -= (int)(mf.DeltaTicksPerQuarterNote * slotWidth / 16);
            }

            ((NoteOnEvent)mf.Events[0][eventIndex]).NoteLength = keepLength;

            GridPanel.Refresh();

        }

        private void NoteAdd_Click(object sender, EventArgs e)
        {
            GridPanel.Cursor = Cursors.Hand;
            addingNote = true;
            noteSelected = false;
            ShowNoteControls();
        }

        private void NoteDel_Click(object sender, EventArgs e)
        {
            mf.Events[0].RemoveAt(eventIndex);
            noteSelected = false;
            ShowNoteControls();
            GridPanel.Refresh();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            MidiFile.Export(selectedPath, mf.Events);
        }

        private void NoteExt_Click(object sender, EventArgs e)
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

            GridPanel.Refresh();
        }

        private void NoteSho_Click(object sender, EventArgs e)
        {
            {
                if (((NoteOnEvent)mf.Events[0][eventIndex]).NoteLength > mf.DeltaTicksPerQuarterNote * slotWidth / 16)
                {
                    ((NoteOnEvent)mf.Events[0][eventIndex]).NoteLength -= (int)(mf.DeltaTicksPerQuarterNote * slotWidth / 16);
                }

                GridPanel.Refresh();
            }
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to remove all notes?", "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                mf = new MidiFile(defaultPath, true);
                GridPanel.Refresh();
            }
        }

        private void NoteVelo_Scroll(object sender, EventArgs e)
        {
            ((NoteOnEvent)mf.Events[0][eventIndex]).Velocity = NoteVelo.Value;
            ((NoteOnEvent)mf.Events[0][eventIndex]).OffEvent.Velocity = NoteVelo.Value;
        }
    }
}