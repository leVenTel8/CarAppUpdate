using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.Entity;
using CarApp.DataModel;



namespace CarApp
{
    public partial class InspectionTableForm : Form
    {
        CarContext db;
        
        int selectRow;
                  
        public InspectionTableForm()
        {
            InitializeComponent();

            db = new CarContext();
            db.Inspections.Load();
            db.Cars.Load();

            dataGridViewInspectionTableForm.DataSource = db.Inspections.Where(p => p.CarId == selectRow).ToList();

        }

        //перегруженный конструктор для получения значения выбранной строки
        public InspectionTableForm(int selectRowMainForm)
        {
            InitializeComponent();

            db = new CarContext();
            db.Cars.Load();

            this.selectRow = selectRowMainForm;                                             //вот так работает рефреш и правльно

            db.Inspections.Where(p => p.CarId == selectRowMainForm).Load();                 // привязывается dataGrid
            dataGridViewInspectionTableForm.DataSource = db.Inspections.Local.ToBindingList();

        }
        

        //кнопка добавления объекта Inspection в таблицу InspectionTableForm
        private void btnAdd_Click(object sender, EventArgs e)
        {
            InspectionForm frmIns = new InspectionForm();
            DialogResult result = frmIns.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;

            Inspection inspection = new Inspection();

            Car addCar = db.Cars.Find(selectRow);

            inspection.DateInspection = frmIns.dateTimePickerDateInspection.Text;
            inspection.NumberInspection = Convert.ToInt32(frmIns.textBoxNumberInspection.Text);
            inspection.Car = (Car)addCar; 

            db.Inspections.Add(inspection);
            db.SaveChanges();

            MessageBox.Show("Объект обновлен");

        }

        //кнопка редактирования выбранного объекта Inspection в таблице InspectionTableForm
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewInspectionTableForm.SelectedRows.Count > 0)
            {
                int index = dataGridViewInspectionTableForm.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridViewInspectionTableForm[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Inspection inspection = db.Inspections.Find(id);

                InspectionForm frmIns = new InspectionForm();
                frmIns.dateTimePickerDateInspection.Text = inspection.DateInspection;
                frmIns.textBoxNumberInspection.Text = Convert.ToString(inspection.NumberInspection);

                DialogResult result = frmIns.ShowDialog(this);

                if (result == DialogResult.Cancel)
                    return;

                inspection.DateInspection = frmIns.dateTimePickerDateInspection.Text;
                inspection.NumberInspection = Convert.ToInt32(frmIns.textBoxNumberInspection.Text);

                db.Entry(inspection).State = EntityState.Modified;
                db.SaveChanges();

                dataGridViewInspectionTableForm.Refresh();
                MessageBox.Show("Объект обновлен");
            }
        }

        //кнопка удаления выбранного объекта Inspection из таблицы InspectionTableForm
        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dataGridViewInspectionTableForm.SelectedRows.Count > 0)
            {

                int index = dataGridViewInspectionTableForm.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridViewInspectionTableForm[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Inspection player = db.Inspections.Find(id);
                db.Inspections.Remove(player);
                db.SaveChanges();

                MessageBox.Show("Объект удален");
                
            }
        }

    }
}
