using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWE005336__Video_Annotation_Software_
{
    class PaintLocker
    {
        private int _LockCount = 0;

        public void Lock()
        {
            _LockCount++;
        }

        public void Unlock()
        {
            _LockCount--;
        }

        public bool Locked { get { return _LockCount > 0; } }
    }
}
