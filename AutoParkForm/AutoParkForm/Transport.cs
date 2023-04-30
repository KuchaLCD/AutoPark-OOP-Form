using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

    namespace AutoParkForm
    {
    

        [Table("Transport")]
        public class Transport : ICalc
        {
            public int RegisterNumberForPark { get; }
            public string Naming { get; }
            public double Mass { get; }
            public double Whidth { get; }
            public DateTime TimeOfRegistrForPark { get; set; }
            public DateTime StayTime { get; set; }
            public string Picture { get; }
            public string Notes { get; }
            public virtual string InfoString()
            {
                string inf = $"\n::::::::::::::::::::::::::::::::::::::::::::::::::::::\n---���������---\n������������: {Naming}" +
                             $"\n����� ����������� � �����: {RegisterNumberForPark}" +
                             $"\n����� ����������� � �����: {TimeOfRegistrForPark}" +
                             $"\n����� ����������(��): {StayTime}" +
                             $"\n�����: {Mass} ��." +
                             $"\n������: {Whidth} �." +
                             $"\n����������: {Notes}" +
                             $"\n::::::::::::::::::::::::::::::::::::::::::::::::::::::";

                return inf;
            }
            public string CalculateOwn()
            {
                double Count1 = TimeOfRegistrForPark.Year * 8760 + TimeOfRegistrForPark.Month * 720 + TimeOfRegistrForPark.Day * 24 + TimeOfRegistrForPark.Hour + TimeOfRegistrForPark.Minute * 0.017 + TimeOfRegistrForPark.Second * 0.00028;
                double Count2 = StayTime.Year * 8760 + StayTime.Month * 720 + StayTime.Day * 24 + StayTime.Hour + StayTime.Minute * 0.017 + StayTime.Second * 0.00028;
                double billForHour = 2;
                double hours = Count2 - Count1;
                double result = billForHour * hours;
                string inf = $"���������� ���������� ����� ������� ��� ���������� ���������� = {hours} �." +
                             $"\n����� ������� = {result} BYN";
                return inf;
            }
            public string CalculateIncome()
            {
                double k = 0;       // ����� ��� �������� ���������� � ����� 
                double billForHour = 2;
                double Count1 = 0;      //������������� ��������
                double Count2 = 0;      //� ��� ����
                //���� "�������" �������� ������� ����� ���������� ����� 
                for (int i = 0; i < ListForTransport.transports.Count; i++)
                {
                    Count1 += ListForTransport.transports[i].TimeOfRegistrForPark.Year * 8760 + ListForTransport.transports[i].TimeOfRegistrForPark.Month * 720 + ListForTransport.transports[i].TimeOfRegistrForPark.Day * 24 + ListForTransport.transports[i].TimeOfRegistrForPark.Hour + ListForTransport.transports[i].TimeOfRegistrForPark.Minute * 0.017 + ListForTransport.transports[i].TimeOfRegistrForPark.Second * 0.00028;
                    Count2 += ListForTransport.transports[i].StayTime.Year * 8760 + ListForTransport.transports[i].StayTime.Month * 720 + ListForTransport.transports[i].StayTime.Day * 24 + ListForTransport.transports[i].StayTime.Hour + ListForTransport.transports[i].StayTime.Minute * 0.017 + ListForTransport.transports[i].StayTime.Second * 0.00028;
                    k++;
                }
                //������� ���� � ��������� ("���� �������� � �����" - "���� �������")
                double hours = Count2 - Count1;
                double result = billForHour * hours;
                string inf = $"���������� ���������� ���������� � ����� = {k}" +
                             $"\n������� = {result} BYN";
                return inf;
            }
            public override string ToString()
            {
                string st = string.Format("��������� {0}, ID {1}", Naming, RegisterNumberForPark);
                return st;
            }
            public Transport(string naming, int registerNumberForPark, double mass, double whidth, DateTime timeOfRegistrForPark, DateTime stayTime, string picture, string notes)
            {
                this.RegisterNumberForPark = registerNumberForPark;
                this.Naming = naming;
                this.Mass = mass;
                this.Whidth = whidth;
                TimeOfRegistrForPark = timeOfRegistrForPark;
                StayTime = stayTime;
                this.Picture = picture;
                this.Notes = notes;
            }
        }
    }