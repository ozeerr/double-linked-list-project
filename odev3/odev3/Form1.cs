using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace odev3
{
    public partial class Form1 : Form
    {

        private ProductList productList = new ProductList();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Product newProduct = new Product
            {
                UrunIsmi = txtUrunIsmi.Text,
                UrunKodu = txtUrunKodu.Text,
                UrunFiyati = txtUrunFiyati.Text
            };
            productList.AddProduct(newProduct);
            ClearTextBoxes();
          
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            ShowProductsInDataGridView();

        }
        private void ShowProductsInDataGridView()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = productList.GetProductList();
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                txtUrunIsmi.Text = selectedRow.Cells["UrunIsmi"].Value.ToString();
                txtUrunKodu.Text = selectedRow.Cells["UrunKodu"].Value.ToString();
                txtUrunFiyati.Text = selectedRow.Cells["UrunFiyati"].Value.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string UrunKoduToUpdate = txtUrunKodu.Text;
            Product updatedProduct = new Product
            {
                UrunIsmi = txtUrunIsmi.Text,
                UrunKodu = txtUrunKodu.Text,
                UrunFiyati = txtUrunFiyati.Text
            };
            productList.UpdateProductByCode(UrunKoduToUpdate, updatedProduct);
            ClearTextBoxes();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            string UrunKoduToDelete = txtUrunKodu.Text;
            productList.DeleteProductByCode(UrunKoduToDelete);
           
            ClearTextBoxes();

        }
        private void ClearTextBoxes()
        {
            txtUrunIsmi.Text = "";
            txtUrunKodu.Text = "";
            txtUrunFiyati.Text = "";
        }

        private void txtUrunIsmi_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                txtUrunIsmi.Text = selectedRow.Cells["UrunIsmi"].Value.ToString();
                txtUrunKodu.Text = selectedRow.Cells["UrunKodu"].Value.ToString();
                txtUrunFiyati.Text = selectedRow.Cells["UrunFiyati"].Value.ToString();
            }
        }

       
    }
    public class Product
    {
        public string UrunIsmi { get; set; }
        public string UrunKodu { get; set; }
        public string UrunFiyati { get; set; }
    }

    public class Node
    {
        public Product Data;
        public Node Next;
        public Node Prev;
    }

    public class ProductList
    {
        private Node head;

        public void AddProduct(Product product)
        {
            Node newNode = new Node { Data = product };
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                Node current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
                newNode.Prev = current;
            }
        }

        public void DeleteProductByCode(string UrunKodu)
        {
            Node nodeToDelete = FindNodeByCode(UrunKodu);
            if (nodeToDelete != null)
            {
                if (nodeToDelete.Prev != null)
                {
                    nodeToDelete.Prev.Next = nodeToDelete.Next;
                }
                if (nodeToDelete.Next != null)
                {
                    nodeToDelete.Next.Prev = nodeToDelete.Prev;
                }
                if (nodeToDelete == head)
                {
                    head = nodeToDelete.Next;
                }
            }
        }

        private Node FindNodeByCode(string code)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Data.UrunKodu == code)
                {
                    return current;
                }
                current = current.Next;
            }
            return null;
        }

        public void UpdateProductByCode(string UrunKodu, Product updatedProduct)
        {
            Node nodeToUpdate = FindNodeByCode(UrunKodu);
            if (nodeToUpdate != null)
            {
                nodeToUpdate.Data = updatedProduct;
            }
        }
        private Node FindNode(Product product)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Data == product)
                {
                    return current;
                }
                current = current.Next;
            }
            return null;
        }
        public List<Product> GetProductList()
        {
            List<Product> productList = new List<Product>();
            Node current = head;
            while (current != null)
            {
                productList.Add(current.Data);
                current = current.Next;
            }
            return productList;
        }
    }
  
}
