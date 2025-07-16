import { Outlet } from "react-router-dom";

import Footer from "./Footer";
import Header from "./Header";

function Layout() {
  return (
    <div className="site-wrapper">
      <Header />

      <main className="main-container">
        <Outlet />
      </main>

      <Footer />
    </div>
  );
}

export default Layout;
