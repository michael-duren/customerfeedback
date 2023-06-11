import { useState } from 'react';
import './HomePage.styles.css';
import LoginForm from '../../components/Forms/LoginForm';
import RegisterForm from '../../components/Forms/RegisterForm';

export default function HomePage() {
  const [loginModal, setLoginModal] = useState(false);
  const [registerModal, setRegisterModal] = useState(false);
  const toggleLogin = () => setLoginModal(!loginModal);
  const toggleRegister = () => setRegisterModal(!registerModal);

  return (
    <div className="container">
      <main>
        <div className="main">
          <h1 className="h2">Welcome.</h1>
          <h2 className="h3">Sign in to get started or register</h2>
          <div className="auth-buttons">
            <button onClick={toggleLogin} className="btn btn-lg btn-primary">
              Login
            </button>
            <button
              onClick={toggleRegister}
              className="btn btn-lg btn-secondary"
            >
              Register
            </button>
          </div>
        </div>
        <div className="aside">
          <img
            className="icons"
            src="/assets/feedback-icons.svg"
            alt="icons of chat bubbles"
          />
        </div>
        {/* modals */}
        <LoginForm toggle={toggleLogin} modal={loginModal} />
        <RegisterForm toggle={toggleRegister} modal={registerModal} />
      </main>
    </div>
  );
}
