import { Link } from "react-router-dom";

function CTAsection() {
  return (
    <section className="card-home-cta">
      <h2>Ready to Get Things Done?</h2>

      <p>Get started today and start crossing off those tasks with ease!</p>

      <Link to="register" className="button Add">
        Add Your First Task
      </Link>
    </section>
  );
}

export default CTAsection;
