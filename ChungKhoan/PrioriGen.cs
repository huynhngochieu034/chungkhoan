using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChungKhoan
{
    class PrioriGen
    {
        public int k;
        public int n;
        public List<string> ResultList; // mảng lưu trữ danh sách kết quả
        public PrioriGen(int k, int n) 
        {
            this.k = k;
            this.n = n;
            ResultList = new List<string>();
        }
        public PrioriGen()
        {

        }
        ///List<int> result = new List<int>();
        int[] result = new int[1000000];
        public void insert()
        {
            // chèn một cấu hình vào danh sách kết quả
            string temp = "";
            for (int i = 1; i <= k; i++)
                temp += result[i] + " ";

            ResultList.Add(temp);
        }

        public void generate()
        {

            // generate cấu hình đầu tiên
            for (int i = 1; i <= k; i++)
            {
                result[i] = 1;
            }

            insert(); // thêm cấu hình vào vào tập kết quả

            // generate cau hình tiếp theo
            int j = k;
            while (result[1] < n)
            {
                if (result[j] == n)
                {
                    j--;
                }
                result[j]++;
                insert(); // thêm cấu hình vào vào tập kết quả
            }

        }
    }
}
