using BusinessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Notes")]
    public class NoteController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllNotes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<NoteDTO>> GetAllNotes()
        {
            List<NoteDTO> NotesList = BusinessLayer.Notes.GetAllNotes();

            if (NotesList.Count == 0)
            {
                return NotFound("No notes found.");
            }

            return Ok(NotesList);
        }

        [HttpGet("AllUser/{id}", Name = "GetAllUserIDNotes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<NoteDTO>> GetAllUserNotesForID(int id)
        {
            List<NoteDTO> NotesList = BusinessLayer.Notes.GetAllUserNotesForID(id);

            if (NotesList.Count == 0)
            {
                return NotFound("No notes found.");
            }

            return Ok(NotesList);
        }

        [HttpGet("{id}", Name = "GetNoteByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<NoteDTO> GetNoteByID(int id)
        {
            if (id < 1)
            {
                return BadRequest($"invalid id: {id}");
            }

            BusinessLayer.Notes note = BusinessLayer.Notes.Find(id);

            if (note == null)
            {
                return NotFound($"Note with id: {id} not found.");
            }

            NoteDTO NDTO = note.NDTO;
            return Ok(NDTO);
        }

        [HttpPost("Add", Name = "AddNewNote")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<NoteDTO> AddNewNote(NoteDTO newNote)
        {
            if (
                newNote == null
                || string.IsNullOrEmpty(newNote.title)
                || string.IsNullOrEmpty(newNote.content)
                || string.IsNullOrEmpty(newNote.state)
                || newNote.categoryID < 0
                || newNote.userID < 0
            )
            {
                return BadRequest("Invalid note data");
            }

            BusinessLayer.Notes note = new BusinessLayer.Notes(
                new NoteDTO(
                    newNote.ID,
                    newNote.userID,
                    newNote.categoryID,
                    newNote.title,
                    newNote.content,
                    newNote.createDate,
                    newNote.updateDate,
                    newNote.state,
                    newNote.isPinned
                )
            );

            note.Save();

            newNote.ID = note.ID;

            return CreatedAtRoute("GetNoteByID", new { id = newNote.ID }, newNote);
        }

        [HttpPut("Update/{id}", Name = "UpdateNote")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<NoteDTO> UpdateNote(int id, NoteDTO updatedNote)
        {
            if (
                id < 1
                || updatedNote == null
                || string.IsNullOrEmpty(updatedNote.title)
                || string.IsNullOrEmpty(updatedNote.content)
                || string.IsNullOrEmpty(updatedNote.state)
                || updatedNote.categoryID < 0
                || updatedNote.userID < 0
            )
            {
                return BadRequest("Invalid note data");
            }

            BusinessLayer.Notes SearchNote = BusinessLayer.Notes.Find(id);

            if (SearchNote == null)
            {
                return NotFound($"Note with id: {id} not found");
            }

            SearchNote.categoryID = updatedNote.categoryID;

            SearchNote.title = updatedNote.title;
            SearchNote.content = updatedNote.content;

            SearchNote.state = updatedNote.state;
            SearchNote.isPinned = updatedNote.isPinned;

            SearchNote.Save();

            return Ok(SearchNote.NDTO);
        }

        [HttpDelete("Delete/{id}", Name = "DeleteNote")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteNote(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid note id");
            }

            if (BusinessLayer.Notes.DeleteNote(id))
            {
                return Ok($"note with id: {id} has been removed successfully.");
            }
            else
            {
                return NotFound($"Note with id: {id} not found");
            }
        }

        //
    }
}
