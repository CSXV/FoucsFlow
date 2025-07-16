import { NavLink } from "react-router-dom";

function Header() {
  // <NavLink to="/login">Account</NavLink>
  // <NavLink to="/notes">Notes</NavLink>
  const loggedIn = localStorage.getItem("isLoggedIn");

  function handleLogout() {
    localStorage.setItem("isLoggedIn", "false");
    localStorage.setItem("userID", "0");
  }

  return (
    <header
      // className="header-container">
      className="card-header"
    >
      <div className="card">
        <NavLink
          // className="site-logo"
          className="button Add"
          to="/"
        >
          <span className="material-symbols-outlined">task_alt</span>
          FocusFlow
        </NavLink>
      </div>

      <nav
        // className="header-nav"
        className="card"
      >
        <NavLink to="/notes">
          <span className="material-symbols-outlined button Add">Notes</span>
        </NavLink>

        {loggedIn == "true" ? (
          <NavLink to={`/account/${localStorage.getItem("userID")}`}>
            <span className="material-symbols-outlined button Add">
              account_circle
            </span>
          </NavLink>
        ) : (
          <NavLink to="/login">
            <span className="material-symbols-outlined button Add">login</span>
          </NavLink>
        )}

        {loggedIn == "true" ? (
          <NavLink to="/login">
            <span
              className="material-symbols-outlined button Cancel"
              onClick={handleLogout}
            >
              logout
            </span>
          </NavLink>
        ) : null}
      </nav>
    </header>
  );
}

export default Header;
