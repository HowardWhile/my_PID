using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIM.Modules
{
    class PIDController
    {
        public PIDController(double P, double I, double D, double second)
        {
            this.Update_PID(P, I, D);
            this.Update_Interval(second);
        }

        public void Update_PID(double P, double I, double D)
        {
            this.kP = P;
            this.kI = I;
            this.kD = D;
        }

        public void Update_Interval(double second)
        {
            this.dT = second;
        }

        public double kP, kI, kD;
        public double dT; // s

        double CtlOnce(double setpoint, double feedback)
        {
            this.error = setpoint - feedback;
            this.integral = this.integral + (error * this.dT);
            this.derivative = (this.error - this.per_err) / this.dT;

            return this.kP * error + this.kI * integral + this.kD * derivative;
        }

        private double integral = 0.0;
        private double derivative = 0.0;
        private double error, per_err;
       


    }
}
