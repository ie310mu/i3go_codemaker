using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IE310.Core.Utils;


namespace IE310.Core.Controls.RibbonStyle
{
    public partial class FormRibbonButtonTest : Form
    {
        public FormRibbonButtonTest()
        {
            InitializeComponent();
        }

        private void ribbonMenuButton44_Click(object sender, EventArgs e)
        {
            I3MessageHelper.ShowInfo("You have press the first zone");
        }

       


        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (IComponent component in this.components.Components)
            {
                if (component.GetType() == typeof(ContextMenuStrip))
                {
                    ((ContextMenuStrip)component).Renderer = new RibbonStyle.RibbonMenuRenderer();
                }
            }

            ShowValues(BasePanel.BackColor);
           
        }

        public void ShowValues(Color onload)
        {
            RibbonColor color = new RibbonColor(onload);

            H.Text = color.GetHue().ToString();
            S.Text = color.GetSaturation().ToString();
            B.Text = color.GetBrightness().ToString();

            HueTrack.Value = (int) color.GetHue();
            SatTrack.Value = (int) color.GetSaturation();
            BriTrack.Value = (int) color.GetBrightness();

        }

        private void ribbonMenuButton31_Click(object sender, EventArgs e)
        {
            I3MessageHelper.ShowInfo("hola");
            
            //RibbonStyle.RibbonMenuShadow shadow = new RibbonStyle.RibbonMenuShadow();
            //shadow.Size = new Size(100, 100);
            //shadow.Show();
        }

        private void ribbonMenuButton31_MouseClick(object sender, MouseEventArgs e)
        {
            I3MessageHelper.ShowInfo("hola");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RibbonStyle.RibbonMenuShadow shadow = new RibbonStyle.RibbonMenuShadow();
            shadow.Size = new Size(200, 200);
            shadow.Show();
        }

        private void ribbonMenuButton1_Click(object sender, EventArgs e)
        {
            I3MessageHelper.ShowInfo("Hi");
        }

        public void ColorUpdate()
        {

            RibbonColor color = new RibbonColor(255,Convert.ToUInt16(H.Text), Convert.ToUInt16(S.Text), Convert.ToUInt16(B.Text));
            switch (_colorsection)
            {
                case ColorSection.Base:

                    BasePanel.BackColor = color.GetColor();
                    foreach (Control control in this.Controls)
                    {
                        if (control.GetType() == typeof(RibbonStyle.RibbonMenuButton))
                        {
                            int alpha = ((RibbonStyle.RibbonMenuButton)control).ColorBase.A;
                            ((RibbonStyle.RibbonMenuButton)control).ColorBase = Color.FromArgb(alpha, BasePanel.BackColor);
                        }
                    }
                    
                break;

                case ColorSection.On:

                    OnPanel.BackColor = color.GetColor();
                foreach (Control control in this.Controls)
                {
                    if (control.GetType() == typeof(RibbonStyle.RibbonMenuButton))
                    {
                        int alpha = ((RibbonStyle.RibbonMenuButton)control).ColorOn.A;
                        ((RibbonStyle.RibbonMenuButton)control).ColorOn = Color.FromArgb(alpha, OnPanel.BackColor);
                    }
                }

                break;

                case ColorSection.Press:

                    PressPanel.BackColor = color.GetColor();
                foreach (Control control in this.Controls)
                {
                    if (control.GetType() == typeof(RibbonStyle.RibbonMenuButton))
                    {
                        int alpha = ((RibbonStyle.RibbonMenuButton)control).ColorPress.A;
                        ((RibbonStyle.RibbonMenuButton)control).ColorPress = Color.FromArgb(alpha, PressPanel.BackColor);
                    }
                }

                break;

                default:
                    BackPanel.BackColor = color.GetColor();
                    this.BackColor = Color.FromArgb(BackPanel.BackColor.R,BackPanel.BackColor.G,BackPanel.BackColor.B);

                    break;
            }
            
            
            
            this.Refresh();
            
        }

        private void BasePanel_BackColorChanged(object sender, EventArgs e)
        {
            BaseText.Text = BasePanel.BackColor.R + ";" + BasePanel.BackColor.G + ";" + BasePanel.BackColor.B;
        }

        private void OnPanel_BackColorChanged(object sender, EventArgs e)
        {
            OnText.Text = OnPanel.BackColor.R + ";" + OnPanel.BackColor.G + ";" + OnPanel.BackColor.B;
        }

        private void PressPanel_BackColorChanged(object sender, EventArgs e)
        {
            PressText.Text = PressPanel.BackColor.R + ";" + PressPanel.BackColor.G + ";" + PressPanel.BackColor.B;
        }

        private void BackPanel_BackColorChanged(object sender, EventArgs e)
        {
            BackText.Text = BackPanel.BackColor.R + ";" + BackPanel.BackColor.G + ";" + BackPanel.BackColor.B;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            _colorsection = ColorSection.Base;
            ShowValues(BasePanel.BackColor);
        }

        public enum ColorSection
        {
            Base,On,Press,Back
        }
        private ColorSection _colorsection = ColorSection.Base;

        private void button2_Click(object sender, EventArgs e)
        {
            _colorsection = ColorSection.On;
            ShowValues(OnPanel.BackColor);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _colorsection = ColorSection.Press;
            ShowValues(PressPanel.BackColor);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _colorsection = ColorSection.Back;
            ShowValues(BackPanel.BackColor);
        }

        
       

       

        private void HueTrack_ValueChanged(object sender, EventArgs e)
        {
            H.Text = HueTrack.Value.ToString(); ColorUpdate();
        }

        private void SatTrack_ValueChanged(object sender, EventArgs e)
        {
            S.Text = SatTrack.Value.ToString(); ColorUpdate();
        }

        private void BriTrack_ValueChanged(object sender, EventArgs e)
        {
            B.Text = BriTrack.Value.ToString(); ColorUpdate();
        }

        private void ribbonMenuButton41_Click(object sender, EventArgs e)
        {
            I3MessageHelper.ShowInfo("You have press the first zone");
        }

        

        

        
        
    }


    public class HSBColor
    {
        uint _oh=0, _os=0, _ob=0; //Origin Space (hsb)
        uint _er=0, _eg=0, _eb=0; //End Space (rgb)
        
        public HSBColor(uint H,uint S,uint B)
        {
            _oh = Math.Min(H,359);
            _os = Math.Min(S,100);
            _ob = Math.Min(B,100);
        }

        public Color RGBColor()
        {
            int conv;
	        double hue, sat, val;
	        int basis;

	        hue = (float)_oh / 100.0f;
	        sat = (float)_os / 100.0f;
	        val = (float)_ob / 100.0f;

	        if ((float)_os == 0) // Gray Colors
		    {
		        conv = (int) (255.0f * val);
                _er = _eg = _eb = (uint) conv;
                return Color.FromArgb((int)_er, (int)_eg, (int)_eb);
		    }

            basis = (int)(255.0f * (1.0 - sat) * val);

            switch ((int)((float)_oh/60.0f))
		{
		case 0:
			_er = (uint)(255.0f * val);
			_eg = (uint)((255.0f * val - basis) * (_oh/60.0f) + basis);
            _eb = (uint)basis;
		break;

		case 1:
			_er = (uint)((255.0f * val - basis) * (1.0f - ((_oh%60)/ 60.0f)) + basis);
			_eg = (uint)(255.0f * val);
            _eb = (uint)basis;
		break;

		case 2:
            _er = (uint)basis;
			_eg = (uint)(255.0f * val);
			_eb = (uint)((255.0f * val - basis) * ((_oh%60)/60.0f) + basis);
		break;
		
		case 3:
            _er = (uint)basis;
			_eg = (uint)((255.0f * val - basis) * (1.0f - ((_oh%60) / 60.0f)) + basis);
			_eb = (uint)(255.0f * val);
		break;
		
		case 4:
			_er = (uint)((255.0f * val - basis) * ((_oh%60) / 60.0f) + basis);
            _eg = (uint)basis;
			_eb = (uint)(255.0f * val);
		break;
		
		case 5:
			_er = (uint)(255.0f * val);
            _eg = (uint)basis;
			_eb = (uint)((255.0f * val - basis) * (1.0f - ((_oh%60) / 60.0f)) + basis);
		break;
		}
        return Color.FromArgb((int)_er, (int)_eg, (int)_eb);
	}

        
    }
}