import './Layout.styles.css';
import NavMenu from '../Nav/NavMenu';

export default function Layout({ children }) {
  return (
    <div className="top-container">
      <div className="layout-content">{children}</div>
    </div>
  );
}
