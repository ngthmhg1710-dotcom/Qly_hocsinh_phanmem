// DayCell.cs
using System;
using System.Drawing;
using System.Windows.Forms;
using DTO;
namespace Calendar // Đã đổi tên
{
    public partial class DayCell : UserControl
    {
        public int Day { get; private set; }

        public DayCell()
        {
            InitializeComponent();
        }

        public void Configure(int day)
        {
            this.Day = day;
            lblDayNumber.Text = day.ToString();
            flpEvents.Controls.Clear();
            this.BackColor = Color.White;
        }

        public void Clear()
        {
            lblDayNumber.Text = "";
            flpEvents.Controls.Clear();
            this.Day = 0;
            this.BackColor = Color.LightGray;
        }

        public void AddEvent(Event anEvent)
        {
            Label lblEvent = new Label
            {
                Text = anEvent.Title,
                BackColor = Color.SkyBlue,
                ForeColor = Color.White,
                Margin = new Padding(1),
                Padding = new Padding(2),
                AutoSize = true,
                Font = new Font("Arial", 8),
                TextAlign = ContentAlignment.MiddleLeft
            };
            flpEvents.Controls.Add(lblEvent);
        }
    }
}