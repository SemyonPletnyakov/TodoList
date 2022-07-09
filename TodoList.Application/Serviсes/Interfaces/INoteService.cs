using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.DTO;

namespace TodoList.Application.Serviсes.Interfaces
{
    public interface INoteService
    {
        public NoteDTO CreateNote(NoteDTO noteDTO);
        public NoteDTO GetNote(int id);
        public List<NoteDTO> GetNoteListByCategoryId(int categoryId);
        public List<NoteDTO> GetNoteListByUserId(string jwt);
        public bool ChangeNote(NoteDTO noteDTO);
        public bool DeleteNote(int id);
    }
}
