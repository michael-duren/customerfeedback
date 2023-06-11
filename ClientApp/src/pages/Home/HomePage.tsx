import { useState } from 'react';
import './HomePage.styles.css';
import LoginForm from '../../components/Forms/LoginForm';
import RegisterForm from '../../components/Forms/RegisterForm';
import { useStore } from '../../stores/store';
import { Link } from 'react-router-dom';

export default function HomePage() {
  const { userStore } = useStore();
  const { user } = userStore;
  const [loginModal, setLoginModal] = useState(false);
  const [registerModal, setRegisterModal] = useState(false);
  const toggleLogin = () => setLoginModal(!loginModal);
  const toggleRegister = () => setRegisterModal(!registerModal);

  return (
    <div className="container">
      <main>
        <div className="main">
          {!user ? (
            <>
              <h1 className="h2">Welcome.</h1>
              <h2 className="h3">Sign in or register to get started</h2>
              <div className="auth-buttons">
                <button
                  onClick={toggleLogin}
                  className="btn btn-lg btn-primary"
                >
                  Login
                </button>
                <button
                  onClick={toggleRegister}
                  className="btn btn-lg btn-secondary"
                >
                  Register
                </button>
              </div>
            </>
          ) : (
            <>
              <h1 className="h2">Welcome Back</h1>
              <h2 className="h3 mt-4">Hello, {user.displayName}</h2>
              <p className="h4">
                Head{' '}
                <Link
                  style={{ textDecoration: 'none' }}
                  className="text-primary"
                  to={'/feedback'}
                >
                  here
                </Link>{' '}
                to leave feedback on this app
              </p>
            </>
          )}
        </div>
        <div className="aside">
          <img
            className="icons"
            src="/assets/feedback-icons.svg"
            alt="icons of chat bubbles"
          />
        </div>
        {/* modals */}
        <LoginForm
          toggle={toggleLogin}
          setLoginModal={setLoginModal}
          modal={loginModal}
        />
        <RegisterForm toggle={toggleRegister} modal={registerModal} />
      </main>
    </div>
  );
}
