import { useState } from 'react';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import LoginForm from '../Forms/LoginForm';
import { Button } from 'reactstrap';
import { useStore } from '../../stores/store';
import { observer } from 'mobx-react-lite';

export default observer(function NavMenu() {
  const { userStore } = useStore();
  const { user, logout } = userStore;
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
              {!user ? (
                <Button onClick={toggleLogin} color="primary">
                  Login
                </Button>
              ) : (
                <Button onClick={() => logout()} className="btn-dark">
                  Logout
                </Button>
              )}
            </li>
          </ul>
        </div>
      </nav>
      <LoginForm
        toggle={toggleLogin}
        setLoginModal={setLoginModal}
        modal={loginModal}
      />
    </header>
  );
});
