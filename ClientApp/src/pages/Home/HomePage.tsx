import './HomePage.styles.css';

export default function HomePage() {
  return (
    <div className="container">
      <main>
        <div className="main">
          <h1 className="h2">Welcome.</h1>
          <h2 className="h3">Sign in to get started or register</h2>
          <div className="auth-buttons">
            <button className="btn btn-lg btn-primary">Login</button>
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
      </main>
    </div>
  );
}
