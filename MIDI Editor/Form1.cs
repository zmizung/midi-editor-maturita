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

        public float pixelsPerTick = 0.5F;
        public float sph = 12;
        //sph = slots per height (počet políček na výšku panelu)
        public int noteSlotSize = 16;
        public int xOffset = 0;
        public int yOffset = 5000;
        // noteSlotSize = 16 znamená, že jedno okénko gridu značí čtvrťovou notu (quarter note)
        // respektive 16 1/64 not (nejmenších možných)

        public Form1()
        {
            InitializeComponent();

            hScrollBar1.Value = xOffset;
            vScrollBar1.Value = yOffset;
            trackBar1.Value = (int)(pixelsPerTick * 100);
            trackBar2.Value = (int)sph;
            comboBox1.SelectedIndex = 4;
        }

        private void Button1_Click(object sender, EventArgs e)

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
        }

        private void Button2_Click(object sender, EventArgs e)
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

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);

            using (Pen penW = new Pen(Color.White))
            using (Pen penG = new Pen (Color.Gray))
            {
                float DeltaTicksPer64thNote = mf.DeltaTicksPerQuarterNote / 16;
                float pixelsPerSlot = DeltaTicksPer64thNote * pixelsPerTick * noteSlotSize;
                float spw = panel1.Width / pixelsPerSlot;
                // SPW = slots per width (počet políček na šířku panelu)
                float xSlotsZoomedOut = (panel1.Width / (float)(mf.DeltaTicksPerQuarterNote / 100F * noteSlotSize / 16));
                xOffset = (int)(xSlotsZoomedOut * hScrollBar1.Value / 10000 * pixelsPerSlot);
                float xOffsetSlots = (float)Math.Floor((double)(xOffset / pixelsPerSlot));
                float xOffsetPx = xOffset - (xOffsetSlots * pixelsPerSlot);

                float pixelsPerPitch = panel1.Height / sph;
                float ySlotsZoomedOut = 127;
                yOffset = (int)(ySlotsZoomedOut * vScrollBar1.Value / 10000 * (pixelsPerPitch));
                float yOffsetSlots = (float)Math.Floor((double)yOffset / (pixelsPerPitch));
                float yOffsetPx = yOffset - (yOffsetSlots * (pixelsPerPitch));

                //tohle jsem si psal sám, je to asi poznat tím jak je to nepřehledný

                if (sph > 24)
                {
                    for (int i = 0; i <= (int)sph; i++)
                    {
                        e.Graphics.DrawLine(penG, 50, 12 * i * (pixelsPerPitch), panel1.Width, 12 * i * (pixelsPerPitch));
                        e.Graphics.DrawLine(penW, 0, 12 * i * (pixelsPerPitch), 50, 12 * i * (pixelsPerPitch));
                    }
                }
                else
                {
                    for (int i = 0; i <= (int)sph; i++)
                    {
                        e.Graphics.DrawLine(penG, 50, i * (pixelsPerPitch) + (pixelsPerPitch - yOffsetPx), panel1.Width, i * (pixelsPerPitch) + ((pixelsPerPitch) - yOffsetPx));
                        e.Graphics.DrawLine(penW, 0, i * (pixelsPerPitch) + (pixelsPerPitch - yOffsetPx), 50, i * (pixelsPerPitch) + ((pixelsPerPitch) - yOffsetPx));
                    }
                }

                e.Graphics.DrawLine(penW, 50, 0, 50, panel1.Height);

                if (pixelsPerTick * noteSlotSize >= 0.9)
                // prostě kontroluje, aby na sebe nebyly sloupce gridu moc nahuštěný
                {
                    
                    for (int i = 0; i <= (spw + 1); i++)
                    {
                        e.Graphics.DrawLine(penG, 50 + i * pixelsPerSlot + (pixelsPerSlot - xOffsetPx), 0, 50 + i * pixelsPerSlot + (pixelsPerSlot - xOffsetPx), panel1.Height);
                    }
                }
            }

            foreach (var track in mf.Events)
            {
                foreach (var midiEvent in track)
                {
                    if (MidiEvent.IsNoteOn(midiEvent))
                    {
                        NoteOnEvent noteOnEvent = (NoteOnEvent)midiEvent;

                        int x = (int)(noteOnEvent.AbsoluteTime * pixelsPerTick);
                        int y = (int)(panel1.Height / sph * noteOnEvent.NoteNumber);
                        int width = (int)(noteOnEvent.NoteLength * pixelsPerTick);
                        int height = (int)(panel1.Height / sph);
                        e.Graphics.FillRectangle(Brushes.Green, 51 + x - xOffset, 1 + y - yOffset, width, height);
                    }
                }
            }
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            pixelsPerTick = trackBar1.Value / 100f;
            panel1.Refresh();
        }

        private void TrackBar2_Scroll(object sender, EventArgs e)
        {
            sph = trackBar2.Value;
            panel1.Refresh();
        }

        private void HScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            panel1.Refresh();
        }

        private void VScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            panel1.Refresh();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {            
            noteSlotSize = 1 * (int)Math.Pow(2, comboBox1.SelectedIndex);
            panel1.Refresh();
        }
    }
}
