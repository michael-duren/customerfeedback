import { useState } from 'react';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import LoginForm from '../Forms/LoginForm';
import { Button } from 'reactstrap';

export default function NavMenu() {
  const [loginModal, setLoginModal] = useState(false);
  const toggleLogin = () => setLoginModal(!loginModal);

  return (
    <header>
      <nav>
        <Link className="h3 logo" to={'/'}>
          Customer Feedback
        </Link>
        <div>
          <ul className="navbar-nav">
            <li>
              <Link to={'/'}>Home</Link>
            </li>
            <li>
              <Link to={'/feedback'}>Feedback</Link>
            </li>
            <li>
              <Button onClick={toggleLogin} color="primary">
                Login
              </Button>
            </li>
          </ul>
        </div>
      </nav>
      <LoginForm toggle={toggleLogin} modal={loginModal} />
    </header>
  );
}
