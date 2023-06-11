import { Outlet } from 'react-router-dom';
import Layout from './components/Layout/Layout';
import './custom.css';
import { ToastContainer } from 'react-toastify';
import NavMenu from './components/Nav/NavMenu';

export default function App() {
  return (
    <Layout>
      <ToastContainer
        position="bottom-right"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme="light"
      />
      <NavMenu />
      <Outlet />
    </Layout>
  );
}
