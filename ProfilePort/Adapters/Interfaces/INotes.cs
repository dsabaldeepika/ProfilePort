using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilePort.Adapters.Interfaces
{
   public interface INotes
    {
        List<Note> GetNote(String UserId);
        Note GetNote(int id);
        Note PostNewNote(string UserId, Note newNote);
        Note PutNote(int id, Note newNote);
        Note DeleteNote(int id);
    }
}
