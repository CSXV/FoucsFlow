function Features() {
  const features = [
    {
      header: "Simple task creation and management",
      text: "Add tasks instantly with smart input and shortcuts.",
      icon: "add_task",
    },

    {
      header: "Customizable due dates, priorities, and reminders",
      text: "Never miss a deadline with built-in alerts.",
      icon: "calendar_month",
    },

    {
      header: "Easy-to-use interface designed for everyone",
      text: "Keep your to-dos sorted by project, priority, or context.",
      icon: "accessibility",
    },

    {
      header: "Stay focused with a distraction-free design",
      text: "Access your tasks from phone, tablet, or desktop.",
      icon: "brush",
    },
  ];

  return (
    <section className="card-home-cta">
      <div>
        <h2>All-in-One Task Manager for a More Organized You</h2>
      </div>

      <div className="card-features">
        {features.map((f, index) => (
          <div key={index} className="card-feature">
            <span className="material-symbols-outlined button">{f.icon}</span>

            <h6>{f.header}</h6>

            <p>{f.text}</p>
          </div>
        ))}
      </div>
    </section>
  );
}

export default Features;
