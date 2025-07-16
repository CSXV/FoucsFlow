using DataAccessLayer;

namespace BusinessLayer
{
    public class Notes
    {
        public enum enMode
        {
            addNew = 0,
            Update = 1,
        };

        public enMode Mode = enMode.addNew;

        public NoteDTO NDTO
        {
            get
            {
                return (
                    new NoteDTO(
                        this.ID,
                        this.userID,
                        this.categoryID,
                        this.title,
                        this.content,
                        this.createDate,
                        this.updateDate,
                        this.state,
                        this.isPinned
                    )
                );
            }
        }

        public int ID { get; set; }
        public int userID { get; set; }
        public int categoryID { get; set; }

        public string title { get; set; }
        public string content { get; set; }

        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }

        public string state { get; set; }
        public bool isPinned { get; set; }

        // constructur
        public Notes(NoteDTO NDTO, enMode cMode = enMode.addNew)
        {
            this.ID = NDTO.ID;
            this.userID = NDTO.userID;
            this.categoryID = NDTO.categoryID;

            this.title = NDTO.title;
            this.content = NDTO.content;

            this.createDate = NDTO.createDate;
            this.updateDate = NDTO.updateDate;

            this.state = NDTO.state;
            this.isPinned = NDTO.isPinned;

            this.Mode = cMode;
        }

        public static Notes Find(int id)
        {
            NoteDTO NDTO = NotesData.GetNoteByID(id);

            if (NDTO != null)
            {
                return new Notes(NDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }

        public static List<NoteDTO> GetAllNotes()
        {
            return NotesData.GetAllNotes();
        }

        public static List<NoteDTO> GetAllUserNotesForID(int id)
        {
            return NotesData.GetAllUserNotesForID(id);
        }

        private bool _AddNewNote()
        {
            this.ID = NotesData.AddNewNote(NDTO);
            return (this.ID != -1);
        }

        private bool _UpdateNoteByID()
        {
            return NotesData.UpdateNoteByID(NDTO);
        }

        public static bool DeleteNote(int id)
        {
            return NotesData.DeleteNoteByID(id);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.addNew:
                    if (_AddNewNote())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateNoteByID();
            }

            return false;
        }
    }
}
