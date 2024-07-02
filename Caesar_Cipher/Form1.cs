using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caesar_Cipher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        char[,] matran = new char[5, 5];
        private void Cipher_Click(object sender, EventArgs e)
        {
            string plaintext = richTextBox1.Text;
            int key = Convert.ToInt32(txtKey.Text);
            char[] cipher_array = plaintext.ToCharArray();
            for (int i = 0; i < cipher_array.Length; i++)
            {
                char letter = cipher_array[i];
                if (letter >= 'A' && letter <= 'Z')
                {
                    letter = (char)(letter - 22);
                    letter = (char)(letter + key);
                    letter = (char)(letter + 22);
                    cipher_array[i] = letter;
                }
                else
                {
                    if (letter == ' ')
                {
                    letter = ' ';
                }
                else
                {
                    letter = (char)(letter + key);
                    if (letter > 'z')
                    {
                        letter = (char)(letter - 26);
                    }
                    else if (letter < 'a')
                    {
                        letter = (char)(letter + 26);
                    }
                    cipher_array[i] = letter;
                }
            }
            }
            plaintext = new string( cipher_array);
            richTextBox1.Text = plaintext;

        }

        private void Decipher_Click(object sender, EventArgs e)
        {
            string plaintext = richTextBox1.Text;
            int key = Convert.ToInt32(txtKey.Text);
            char[] cipher_array = plaintext.ToCharArray();
            for (int i = 0; i < cipher_array.Length; i++)
            {
                char letter = cipher_array[i];
                if (letter >= 'A' && letter <= 'Z')
                {
                    letter = (char)(letter - 22);
                    letter = (char)(letter - key);
                    letter = (char)(letter + 22);
                    cipher_array[i] = letter;
                }
                else
                {
                    if (letter == ' ')
                    {
                        letter = ' ';
                    }
                    else
                    {
                        letter = (char)(letter - key);
                        if (letter > 'z')
                        {
                            letter = (char)(letter - 26);
                        }
                        else if (letter < 'a')
                        {
                            letter = (char)(letter + 26);
                        }
                        cipher_array[i] = letter;
                    }
                }
            }
            plaintext = new string(cipher_array);
            richTextBox1.Text = plaintext;
        }

        private void open_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Rich Text Files(*.rtf)|*rtf|All File (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.RichText);
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Rich Text Files(*.rtf)|*rtf | All File (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnMatrix_Click(object sender, EventArgs e)
        {
            string key = ppkey.Text;
            key = key.ToUpper();
            char[] khoa = key.ToCharArray();
            
            string defaultKeySquare = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
            key = key.Replace("J", "");
            key += defaultKeySquare;
            for (int i = 0; i < 25; ++i)
            {
                List<int> indexes = FindAllOccurrences(key, defaultKeySquare[i]);
                key = RemoveAllDuplicates(key, indexes);
            }
            key = key.Substring(0, 25);

            for (int i = 0; i < 25; ++i)
            {
                matran[i / 5, (i % 5)] = key[i];
                
            }
            for (int i = 0; i < matran.GetLength(0); i++)
            {
                for (int j = 0; j < matran.GetLength(1); j++)
                {
                    matrix.AppendText(matran[i, j].ToString() + "   ");
                }
                matrix.AppendText(Environment.NewLine);
            }
            string strInput = input.Text;
            if (strInput.Length % 2 != 0)
                strInput += 'X';
            char[] plaintext = strInput.ToCharArray();
            for (int i = 0; i < matran.GetLength(0); i++)
            {
                for (int j = 0; j < matran.GetLength(1); j++)
                {

                }
            }
            string strOutput = output.Text;



        }
       
       
        private string RemoveAllDuplicates(string key, List<int> indexes)
        {
            string retVal = key;

            for (int i = indexes.Count - 1; i >= 1; i--)
                retVal = retVal.Remove(indexes[i], 1);

            return retVal; 
        }

        private List<int> FindAllOccurrences(string key, char value)
        {
            List<int> indexes = new List<int>();

            int index = 0;
            while ((index = key.IndexOf(value, index)) != -1)
                indexes.Add(index++);

            return indexes;
        }
        private void FindCharPosition(char c, ref int row, ref int col)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (matran[i, j] == c)
                    {
                        row = i;
                        col = j;
                        return;
                    }
                }
            }
        }

        private void btnCipher_Click(object sender, EventArgs e)
        {
            string plaintext = input.Text;
            string processedText;
            processedText = ProcessText(plaintext);
            string encryptedText = "";

            for (int i = 0; i < processedText.Length; i = i+2)
            {
                char firstChar = processedText[i];
                int j = i;
                char secondChar = processedText[j + 1];

                int firstRow = 0, firstCol = 0, secondRow = 0, secondCol = 0;

                // Tìm vị trí của các ký tự trong ma trận
                FindCharPosition(firstChar, ref firstRow, ref firstCol);
                FindCharPosition(secondChar, ref secondRow, ref secondCol);

                // Mã hóa các ký tự
                if (firstRow == secondRow)
                {
                    firstCol = (firstCol + 1) % 5;
                    secondCol = (secondCol + 1) % 5;
                }
                else if (firstCol == secondCol)
                {
                    firstRow = (firstRow + 1) % 5;
                    secondRow = (secondRow + 1) % 5;
                }
                else
                {
                    int temp = firstCol;
                    firstCol = secondCol;
                    secondCol = temp;
                }

                // Thêm các ký tự đã mã hóa vào chuỗi kết quả
                encryptedText += matran[firstRow, firstCol];
                encryptedText += matran[secondRow, secondCol];
            }
            output.Text = encryptedText;
        }
        private string ProcessText(string text)
        {
            // Loại bỏ các ký tự không phải chữ cái và thay thế J bằng I
            string processedText = new String(text.ToUpper().Where(c => Char.IsLetter(c) && c != 'J').ToArray());
            for (int i = 1; i < processedText.Length; i += 2)
            {
                if (processedText[i] == processedText[i - 1])
                {
                    processedText = processedText.Insert(i, "X");
                    //i = i + 1;
                }
            }
            // Thêm ký tự 'X' nếu độ dài của chuỗi là lẻ
            if (processedText.Length % 2 != 0)
            {
                processedText += 'X';
            }

            // Thay thế các cặp ký tự giống nhau bằng cặp ký tự đó và 'X'
            

            return processedText;
        }

        private void btnDecipher_Click(object sender, EventArgs e)
        {
            string processedText = ProcessText(output.Text);
            string decryptedText = "";

            for (int i = 0; i < processedText.Length; i += 2)
            {
                char firstChar = processedText[i];
                char secondChar = processedText[i + 1];

                int firstRow = 0, firstCol = 0, secondRow = 0, secondCol = 0;

                // Tìm vị trí của các ký tự trong ma trận
                FindCharPosition(firstChar, ref firstRow, ref firstCol);
                FindCharPosition(secondChar, ref secondRow, ref secondCol);

                // Giải mã các ký tự
                if (firstRow == secondRow)
                {
                    firstCol = (firstCol - 1 + 5) % 5;
                    secondCol = (secondCol - 1 + 5) % 5;
                }
                else if (firstCol == secondCol)
                {
                    firstRow = (firstRow - 1 + 5) % 5;
                    secondRow = (secondRow - 1 + 5) % 5;
                }
                else
                {
                    int temp = firstCol;
                    firstCol = secondCol;
                    secondCol = temp;
                }

                // Thêm các ký tự đã giải mã vào chuỗi kết quả
                decryptedText += matran[firstRow, firstCol];
                decryptedText += matran[secondRow, secondCol];
            }
            input.Text = decryptedText;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            

        }

        private void V_cipher_Click(object sender, EventArgs e)
        {
            if(txtVtext.Text =="" && txtKey.Text =="")
            {
                MessageBox.Show("Moi nhap day du thong tin!");
            }
            else
            {
                string encryptedText = txtVtext.Text;
                encryptedText = encryptedText.ToUpper();
                string key = txtVKey.Text;
                key = key.ToUpper();
                txtVresult.Text = VigenereEncrypt(encryptedText, key);
            }
            
        }

        static string VigenereEncrypt(string plaintext, string key)
        {
            string encryptedText = "";
            int keyIndex = 0;

            foreach (char c in plaintext)
            {
                if (!char.IsLetter(c))
                {
                    encryptedText += c;
                    continue;
                }

                char encryptedChar = (char)(((c - 'A') + (char.ToUpper(key[keyIndex]) - 'A')) % 26 + 'A');
                encryptedText += encryptedChar;

                keyIndex++;
                if (keyIndex == key.Length)
                    keyIndex = 0;
            }

            return encryptedText;
        }

        static string VigenereDecrypt(string ciphertext, string key)
        {
            string decryptedText = "";
            int keyIndex = 0;

            foreach (char c in ciphertext)
            {
                if (!char.IsLetter(c))
                {
                    decryptedText += c;
                    continue;
                }

                char decryptedChar = (char)(((c - 'A') - (char.ToUpper(key[keyIndex]) - 'A') + 26) % 26 + 'A');
                decryptedText += decryptedChar;

                keyIndex++;
                if (keyIndex == key.Length)
                    keyIndex = 0;
            }

            return decryptedText;
        }

        private void V_De_Click(object sender, EventArgs e)
        {
            if (txtVresult.Text == "" && txtKey.Text == "")
            {
                MessageBox.Show("Thong tin chua day du!");
            }
            else
            {
                string encryptedText = txtVresult.Text;
                encryptedText = encryptedText.ToUpper();
                string key = txtVKey.Text;
                key = key.ToUpper();
                txtVtext.Text = VigenereDecrypt(encryptedText, key);
            }
        }

        private void btnRcipher_Click(object sender, EventArgs e)
        {
            int key;
            if (txtRtext.Text == "" && txtRkey.Text == "")
            {
                MessageBox.Show("Thong tin chua day du!");
            }
            else
            {
                try
                {
                    if(int.TryParse(txtRkey.Text, out key))
                    {
                        string plaintext = txtRtext.Text;
                        txtRresult.Text = RailFenceEncrypt(plaintext, key);
                    }
                }catch
                {

                }
               
            }
        }
        public static string RailFenceEncrypt(string plaintext, int rails)
        {
            char[,] matrix = new char[rails, plaintext.Length];
            int row = 0;
            int col = 0;
            bool isGoingDown = false;

            for (int i = 0; i < plaintext.Length; i++)
            {
                if (row == 0 || row == rails - 1)
                    isGoingDown = !isGoingDown;

                matrix[row, col] = plaintext[i];

                if (isGoingDown)
                    row++;
                else
                {
                    row--;
                    col++;
                }
            }

            string ciphertext = "";
            for (int i = 0; i < rails; i++)
            {
                for (int j = 0; j < plaintext.Length; j++)
                {
                    if (matrix[i, j] != '\0')
                        ciphertext += matrix[i, j];
                }
            }

            return ciphertext;
        }

        public static string RailFenceDecrypt(string ciphertext, int rails)
        {
            char[,] matrix = new char[rails, ciphertext.Length];
            int row = 0;
            int col = 0;
            bool isGoingDown = false;

            for (int i = 0; i < ciphertext.Length; i++)
            {
                if (row == 0 || row == rails - 1)
                    isGoingDown = !isGoingDown;

                matrix[row, col] = '*';

                if (isGoingDown)
                    row++;
                else
                {
                    row--;
                    col++;
                }
            }

            int index = 0;
            for (int i = 0; i < rails; i++)
            {
                for (int j = 0; j < ciphertext.Length; j++)
                {
                    if (matrix[i, j] == '*' && index < ciphertext.Length)
                    {
                        matrix[i, j] = ciphertext[index];
                        index++;
                    }
                }
            }

            string plaintext = "";
            row = 0;
            col = 0;
            isGoingDown = false;

            for (int i = 0; i < ciphertext.Length; i++)
            {
                if (row == 0 || row == rails - 1)
                    isGoingDown = !isGoingDown;

                plaintext += matrix[row, col];

                if (isGoingDown)
                    row++;
                else
                {
                    row--;
                    col++;
                }
            }

            return plaintext;
        }

        private void btnRDeciper_Click(object sender, EventArgs e)
        {
            int key;
            if (txtRresult.Text == "" && txtRkey.Text == "")
            {
                MessageBox.Show("Thong tin chua day du!");
            }
            else
            {
                try
                {
                    if (int.TryParse(txtRkey.Text, out key))
                    {
                        string plaintext = txtRresult.Text;
                        txtRtext.Text = RailFenceDecrypt(plaintext, key);
                        txtRtext.ForeColor = Color.Red;

                    }
                }
                catch
                {

                }

            }
            
        }
        static string[] SplitStringIntoRows(string input, int rowLength)
        {
            int numRows = (int)Math.Ceiling((double)input.Length / rowLength);
            string[] rows = new string[numRows];

            for (int i = 0; i < numRows; i++)
            {
                int startIndex = i * rowLength;
                int length = Math.Min(rowLength, input.Length - startIndex);
                rows[i] = input.Substring(startIndex, length);
            }

            return rows;
        }
        private void btnHcipher_Click(object sender, EventArgs e)
        {
            string plaintext = txtHplaintext.Text;
            char[] word = plaintext.ToCharArray();
            int total = word.Length;
            int key = 0;
            int hang = 0;
            
            string Result = "";
            if (int.TryParse(txtHkey.Text, out key))
            {
                if (total % key != 0)
                {
                    hang = total / key;
                    hang = hang + 1;
                    int i = 0;
                    int j = 0;
                    
                    while (j < total)
                    {
                        
                        for (i = 0; i < key; i++)
                        {
                            if (word != null)
                            {
                                
                                if (j < total)
                                {
                                    richTextBox2.Text += word[j];
                                    j++;
                                }
                                    
                                else
                                    break;
                            }
                            else
                                break;
                        }
                        richTextBox2.Text += "\n";
                    }
                    char[,] matrix = new char[hang, key];
                    int k = 0;
                    foreach (string line in richTextBox2.Lines)
                    {
                        char[] lines = line.ToCharArray();
                        for ( i = 0; i < lines.Length; i++)
                        {
                            matrix[k, i] = lines[i];
                        }
                        k += 1;
                    }
                    string keyword = txtSTT.Text;
                    char[] STT = keyword.ToCharArray();
                    List<int> stt = new List<int>();
                    foreach (char item in STT)
                    {
                        stt.Add(int.Parse(item.ToString()));
                    }

                    foreach (int item in stt)
                    {
                        for (j = 0; j < matrix.GetLength(1); j++)
                        {
                            for (i = 0; i < matrix.GetLength(0); i++)
                            {
                            
                                if(item == j)
                                {
                                    richTextBox3.Text += matrix[i, item];
                                }
                            }


                    }
                    }
                }

            }
            

        }

        private void btnHdecipher_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            string plaintext = richTextBox3.Text;
            char[] word = plaintext.ToCharArray();
            int total = word.Length;
            int key = 0;
            int hang = 0;

            string Result = "";
            if (int.TryParse(txtHkey.Text, out key))
            {
                if (total % key != 0)
                {
                    hang = total / key;
                    hang = hang + 1;
                    int i = 0;
                    int j = 0;
                    int k = 0;
                    char[,] matrix = new char[hang, key];
                    while(k<matrix.Length)
                    {
                        for (j = 0; j < matrix.GetLength(1); j++)
                        {
                            for (i = 0; i < matrix.GetLength(0); i++)
                            {
                                matrix[i, j] = word[k];
                                k++;
                            }

                        }
                    }
                       
                    
                    for(i = 0;i<matrix.GetLength(0);i++)
                    {
                        for (j = 0; j < matrix.GetLength(1); j++)
                            richTextBox3.Text += matrix[i, j].ToString();
                    }
                    
                    
                }

            }



        }

        private void Cipher_Click_1(object sender, EventArgs e)
        {

        }

        private void open_Click_1(object sender, EventArgs e)
        {

        }

        private void matrix_TextChanged(object sender, EventArgs e)
        {

        }

        static int vitri, vitridaucach;
        string tutruoc;
        private void but_mahoa_Click(object sender, EventArgs e)
        {
            if(matrankhoa.Text == "2")
            {
                int a, b, c, d;
                a = Convert.ToInt32(k_1.Text);
                b = Convert.ToInt32(k_2.Text);
                c = Convert.ToInt32(k_4.Text);
                d = Convert.ToInt32(k_5.Text);
                vitri = txt_ro.Text.LastIndexOf(" ");
                tutruoc = txt_ro.Text.Substring(vitri);
                vitridaucach = txt_ro.Text.IndexOf(tutruoc);
                string banro = txt_ro.Text.ToUpper().Replace(" ", "");
                int[,] key = { { a, b }, { c, d } };
                int modulo = 26;
                string banma = "";
                if (banro.Length % 2 != 0)
                {
                    banro += 'X';
                }

                for (int i = 0; i < banro.Length; i += 2)
                {
                    int p1 = banro[i] - 'A';
                    int p2 = banro[i + 1] - 'A';

                    int c1 = (key[0, 0] * p1 + key[0, 1] * p2) % modulo;
                    int c2 = (key[1, 0] * p1 + key[1, 1] * p2) % modulo;

                    banma += (char)(c1 + 'A');
                    banma += (char)(c2 + 'A');

                }
                txt_ma.Text = banma;
            }
            else if(matrankhoa.Text == "3")
            {
                int a, b, c, d, e1, f, g, h, k, i1, j1, i2, j2, i3, j3;
                a = Convert.ToInt32(k_1.Text);
                b = Convert.ToInt32(k_2.Text);
                c = Convert.ToInt32(k_3.Text);
                d = Convert.ToInt32(k_4.Text);
                e1 = Convert.ToInt32(k_5.Text);
                f = Convert.ToInt32(k_6.Text);
                g = Convert.ToInt32(k_7.Text);
                h = Convert.ToInt32(k_8.Text);
                k = Convert.ToInt32(k_9.Text);

                vitri = txt_ro.Text.LastIndexOf(" ");
                tutruoc = txt_ro.Text.Substring(vitri);
                vitridaucach = txt_ro.Text.IndexOf(tutruoc);
                string banro = txt_ro.Text.ToUpper().Replace(" ", "");

                int[,] key = { { a, b, c }, { d, e1, f }, { g, h, k } };
                int modulo = 26;
                string banma = "";

                if (banro.Length % 3 == 2)
                {
                    banro += 'X';
                }
                else if(banro.Length % 3 == 1)
                {
                    banro += 'X' + 'X';
                }

                for (int i = 0; i < banro.Length; i += 3)
                {
                    int p1 = banro[i] - 'A';
                    int p2 = banro[i + 1] - 'A';
                    int p3 = banro[i + 2] - 'A';

                    int c1 = (key[0, 0] * p1 + key[0, 1] * p2 + key[0, 2] * p3) % modulo;
                    int c2 = (key[1, 0] * p1 + key[1, 1] * p2 + key[1, 2] * p3) % modulo;
                    int c3 = (key[2, 0] * p1 + key[2, 1] * p2 + key[2, 2] * p3) % modulo;

                    banma += (char)(c1 + 'A');
                    banma += (char)(c2 + 'A');
                    banma += (char)(c3 + 'A');
                }
                txt_ma.Text = banma;
            }
        }

        static int ModNgichDao(int a, int m)
        {
            a = a % m;
            for (int x = 1; x < m; x++)
            {
                if ((a * x) % m == 1)
                    return x;
            }
            return 1;
        }

        private void but_giaima_Click(object sender, EventArgs e)
        {
            if(matrankhoa.Text == "3")
            {
                int a, b, c, d, e1, f, g, h, k, i1, j1, i2, j2, i3, j3;
                a = Convert.ToInt32(k_1.Text);
                b = Convert.ToInt32(k_2.Text);
                c = Convert.ToInt32(k_3.Text);
                d = Convert.ToInt32(k_4.Text);
                e1 = Convert.ToInt32(k_5.Text);
                f = Convert.ToInt32(k_6.Text);
                g = Convert.ToInt32(k_7.Text);
                h = Convert.ToInt32(k_8.Text);
                k = Convert.ToInt32(k_9.Text);
                //i1 = Convert.ToInt32(k_10.Text);
                //j1 = Convert.ToInt32(k_11.Text);
                //i2 = Convert.ToInt32(k_12.Text);
                //j2 = Convert.ToInt32(k_13.Text);
                //i3 = Convert.ToInt32(k_14.Text);
                //j3 = Convert.ToInt32(k_15.Text);

                string banma = txt_ma.Text.ToUpper().Replace(" ", "");
                int[,] key = { { a, b, c }, { d, e1, f }, { g, h, k } };
                int modulo = 26;
                string banro = "";

                int khoadao = (key[0, 0] * key[1, 1] * key[2, 2] - key[0, 2] * key[1, 1] * key[2, 0] + modulo) % modulo;

                int dinhthucnghichdao = ModNgichDao(khoadao, modulo);

                int temp = key[0, 0];
                key[0, 0] = key[2, 2];
                key[2, 2] = temp;
                key[0, 2] = (modulo - key[0, 2]) % modulo;
                key[2, 0] = (modulo - key[2, 0]) % modulo;

                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        key[i, j] = (key[i, j] * dinhthucnghichdao) % modulo;

                int[,] keytam = key;

                for (int i = 0; i < banma.Length; i += 3)
                {
                    int c1 = banma[i] - 'A';
                    int c2 = banma[i + 1] - 'A';
                    int c3 = banma[i + 2] - 'A';

                    int p1 = (keytam[0, 0] * c1 + keytam[0, 1] * c2 + keytam[0, 2] * c3) % modulo;
                    int p2 = (keytam[1, 0] * c1 + keytam[1, 1] * c2 + keytam[1, 2] * c3) % modulo;
                    int p3 = (keytam[2, 0] * c1 + keytam[2, 1] * c2 + keytam[2, 2] * c3) % modulo;

                    banro += (char)(p1 + 'A');
                    banro += (char)(p2 + 'A');
                    banro += (char)(p3 + 'A');

                    //if (banro.EndsWith("X"))
                    //{
                    //    banro = banro.Remove(banro.Length - 1);
                    //    banro = banro.Insert(vitridaucach, " ");
                    //}
                    //else if (banro.EndsWith("X"))
                    //{
                    //    banro = banro.Remove(banro.Length - 2);
                    //    banro = banro.Insert(vitridaucach, " ");
                    //}
                }

                txt_ro.Text = banro;

                kd1.Text = keytam[0, 0].ToString();
                kd2.Text = keytam[0, 1].ToString();
                kd3.Text = keytam[0, 2].ToString();
                kd4.Text = keytam[1, 0].ToString();
                kd5.Text = keytam[1, 1].ToString();
                kd6.Text = keytam[1, 2].ToString();
                kd7.Text = keytam[2, 0].ToString();
                kd8.Text = keytam[2, 1].ToString();
                kd9.Text = keytam[2, 2].ToString();
            }

            if(matrankhoa.Text == "2")
            {
                int a, b, c, d;
                a = Convert.ToInt32(k_1.Text);
                b = Convert.ToInt32(k_2.Text);
                c = Convert.ToInt32(k_4.Text);
                d = Convert.ToInt32(k_5.Text);

                string banma = txt_ma.Text.ToUpper().Replace(" ", "");
                int[,] key = { { a, b }, { c, d } };
                int modulo = 26;
                string banro = "";

                int khoadao = (key[0, 0] * key[1, 1] - key[0, 1] * key[1, 0] + modulo) % modulo;
                int dinhthucnghichdao = ModNgichDao(khoadao, modulo);

                int temp = key[0, 0];
                key[0, 0] = key[1, 1];
                key[1, 1] = temp;
                key[0, 1] = (modulo - key[0, 1]) % modulo;
                key[1, 0] = (modulo - key[1, 0]) % modulo;

                for (int i = 0; i < 2; i++)
                    for (int j = 0; j < 2; j++)
                        key[i, j] = (key[i, j] * dinhthucnghichdao) % modulo;

                int[,] keytam = key;

                for (int i = 0; i < banma.Length; i += 2)
                {
                    int c1 = banma[i] - 'A';
                    int c2 = banma[i + 1] - 'A';

                    int p1 = (keytam[0, 0] * c1 + keytam[0, 1] * c2) % modulo;
                    int p2 = (keytam[1, 0] * c1 + keytam[1, 1] * c2) % modulo;

                    banro += (char)(p1 + 'A');
                    banro += (char)(p2 + 'A');

                    if (banro.EndsWith("X"))
                    {
                        banro = banro.Remove(banro.Length - 1);
                        banro = banro.Insert(vitridaucach, " ");
                    }

                    txt_ro.Text = banro;
                    kd1.Text = keytam[0, 0].ToString();
                    kd2.Text = keytam[0, 1].ToString();
                    kd4.Text = keytam[1, 0].ToString();
                    kd5.Text = keytam[1, 1].ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txt_ro.Text = "";
            txt_ma.Text = "";

            k_1.Text = "";
            k_2.Text = "";
            k_4.Text = "";
            k_5.Text = "";

            txt_ro.Focus();
        }

        private void matrankhoa_TextChanged(object sender, EventArgs e)
        {
            if(matrankhoa.Text == "2")
            {
                k_1.Visible = true;
                k_2.Visible = true;
                k_4.Visible = true;
                k_5.Visible = true;

                kd1.Visible = true;
                kd2.Visible = true;
                kd4.Visible = true;
                kd5.Visible = true;
            }
            else if (matrankhoa.Text == "3")
            {
                k_1.Visible = true;
                k_2.Visible = true;
                k_3.Visible = true;
                k_4.Visible = true;
                k_5.Visible = true;
                k_6.Visible = true;
                k_7.Visible = true;
                k_8.Visible = true;
                k_9.Visible = true;

                kd1.Visible = true;
                kd2.Visible = true;
                kd3.Visible = true;
                kd4.Visible = true;
                kd5.Visible = true;
                kd6.Visible = true;
                kd7.Visible = true;
                kd8.Visible = true;
                kd9.Visible = true;
            }
            else
            {
                k_1.Visible = false;
                k_2.Visible = false;
                k_3.Visible = false;
                k_4.Visible = false;
                k_5.Visible = false;
                k_6.Visible = false;
                k_7.Visible = false;
                k_8.Visible = false;
                k_9.Visible = false;

                kd1.Visible = false;
                kd2.Visible = false;
                kd3.Visible = false;
                kd4.Visible = false;
                kd5.Visible = false;
                kd6.Visible = false;
                kd7.Visible = false;
                kd8.Visible = false;
                kd9.Visible = false;
            }
        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            k_1.Visible = false;
            k_2.Visible = false;
            k_3.Visible = false;
            k_4.Visible = false;
            k_5.Visible = false;
            k_6.Visible = false;
            k_7.Visible = false;
            k_8.Visible = false;
            k_9.Visible = false;

            kd1.Visible = false;
            kd2.Visible = false;
            kd3.Visible = false;
            kd4.Visible = false;
            kd5.Visible = false;
            kd6.Visible = false;
            kd7.Visible = false;
            kd8.Visible = false;
            kd9.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

}


