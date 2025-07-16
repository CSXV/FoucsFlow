import { Link } from "react-router-dom";

function Hero() {
  return (
    <section className="card-home-hero">
      <h1>
        Stay in your zone, <br />
        get things done
      </h1>

      <p>
        Stay on top of your tasks with a simple, powerful to-do app built to
        help you focus and get things done.
      </p>

      <Link to="register" className="button Add">
        Start now
      </Link>
    </section>
  );
}

export default Hero;
