import Hero from "./HomePage/Hero";
import Features from "./HomePage/Features";
import WhyFocusFlow from "./HomePage/WhyFocusFlow";
import CTAsection from "./HomePage/CTAsection";

function Home() {
  return (
    <div className="home-container">
      <Hero />
      <Features />
      <WhyFocusFlow />
      <CTAsection />
    </div>
  );
}

export default Home;
