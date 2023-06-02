import './Layout.styles.css';
import NavMenu from './NavMenu.tsx';

export default function Layout({ children }) {
  return (
    <div className="top-container">
      <NavMenu />
      <div className="layout-content">{children}</div>
    </div>
  );
}
