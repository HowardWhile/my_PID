using AIM.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace my_pid
{
    public partial class Form1 : Form
    {
        PIDController pid_ctrl;
        public Form1()
        {
            InitializeComponent();
            this.pid_ctrl = new PIDController(
                (double)this.num_kp.Value,
                (double)this.num_ki.Value,
                (double)this.num_kd.Value,
                tbar_interval_ms.Value / 1000.0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chart_pid_Load(object sender, EventArgs e)
        {

        }

        private void num_ValueChanged(object sender, EventArgs e)
        {
            this.pid_ctrl.Update_PID(
                (double)this.num_kp.Value,
                (double)this.num_ki.Value,
                (double)this.num_kd.Value);
        }

        private void tbar_interval_ms_Scroll(object sender, EventArgs e)
        {
            int ms = tbar_interval_ms.Value;
            this.pid_ctrl.Update_Interval(ms / 1000.0);
            this.tmr_pid_controller.Interval = tbar_interval_ms.Value;

            this.gbox_interval.Text = $"Interval = {ms}ms";
        }

        private void tmr_pid_controller_Tick(object sender, EventArgs e)
        {           
            this.controller_output = this.pid_ctrl.Update_Once(this.tbar_setpoint.Value, this.plant_feedback);
        }

        private void tbar_setpoint_Scroll(object sender, EventArgs e)
        {
            this.gbox_setpoint.Text = $"Setpoint = {this.tbar_setpoint.Value}";
        }

        double controller_output = 0.0;
        double plant_feedback = 0.0;
        private void tmr_plant_sim_Tick(object sender, EventArgs e)
        {
            double PV = this.plant_feedback;

            PV = PV + (this.controller_output * 0.20) - (PV * 0.10);

            this.plant_feedback = PV;
        }
    }
}
