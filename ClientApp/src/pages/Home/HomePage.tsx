import { useState } from 'react';
import GenericModal from '../../components/Modals/GenericModal';
import './HomePage.styles.css';
import LoginForm from '../../components/Forms/LoginForm';

export default function HomePage() {
  const [loginModal, setLoginModal] = useState(false);
  const toggleLogin = () => setLoginModal(!loginModal);

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
            <button className="btn btn-lg btn-secondary">Register</button>
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
        <GenericModal toggle={toggleLogin} modal={loginModal}>
          <LoginForm />
        </GenericModal>
        {/* login */}
        <div></div>
      </main>
    </div>
  );
}
