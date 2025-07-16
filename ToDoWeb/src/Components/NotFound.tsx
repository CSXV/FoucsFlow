import { Link } from "react-router-dom";

function NotFound() {
  return (
    <div className="not-found-container">
      <h2>Sorry, the page you were looking for was not found.</h2>

      <Link className="link-button" to="/">
        Retrun to home
      </Link>
    </div>
  );
}

export default NotFound;
