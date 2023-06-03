import { useState } from 'react';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export default function NavMenu() {
  const [toggleNavBar, setToggleNavBar] = useState(false);

  const onToggleNavbar = (): void => {
    setToggleNavBar(!toggleNavBar);
  };

  return (
    <header>
      <nav>
        <Link className="h3 logo" to={'/'}>
          Customer Feedback
        </Link>
        <div onClick={onToggleNavbar} className="mr-2" />
        <div>
          <ul className="navbar-nav">
            <li>
              <Link to={'/'}>Home</Link>
            </li>
            <li>
              <Link to={'/feedback'}>Feedback</Link>
            </li>
            <li>
              <button className="btn btn-primary">Log In</button>
            </li>
          </ul>
        </div>
      </nav>
    </header>
  );
}
