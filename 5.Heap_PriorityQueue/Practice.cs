using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5.Heap_PriorityQueue
{
    #region 응급실 우선순위 치료시스템
    //응급실 우선순위 치료 시스템
    //골든타임이 짧은 순서대로 치료
    public class EmergencySystem
    {
        private DataStructure.PriorityQueue<string,int> patientList = new DataStructure.PriorityQueue<string,int>();

        public int Count { get { return patientList.Count; } }

        public void AddPatient(string name, int goldenTime) 
        {
            patientList.Enqueue(name, goldenTime);
        }

        public void Healing() 
        {
            if (patientList.Count < 0)
            {
                Console.WriteLine("환자가 없습니다.");
                return;
            }

            Console.WriteLine("{0} 환자 치료 했습니다 (우선순위: {1})", patientList.TopElement,patientList.TopPriority);
            patientList.Dequeue();
        }
    }
    #endregion
}
