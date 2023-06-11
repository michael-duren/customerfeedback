import { Outlet } from 'react-router-dom';
import Layout from './components/Layout/Layout';
import './custom.css';
import { ToastContainer } from 'react-toastify';
import NavMenu from './components/Nav/NavMenu';
import { useStore } from './stores/store';
import { useEffect } from 'react';

export default function App() {
  const { userStore, commonStore } = useStore();

  useEffect(() => {
    if (commonStore.token) {
      userStore.getUser().finally(() => commonStore.setAppLoaded());
    } else {
      commonStore.setAppLoaded();
    }
  }, [commonStore, userStore]);

  // if (!commonStore.appLoaded)
  //   return (
  //     <div
  //       style={{ minHeight: '100vh' }}
  //       className="d-flex flex-column w-100 h-100 align-items-center justify-content-center"
  //     >
  //       <div
  //         style={{ width: '3rem', height: '3rem' }}
  //         className="spinner-border text-primary"
  //         role="status"
  //       >
  //         <span className="visually-hidden">Loading...</span>
  //       </div>
  //       <div className="mt-4">Loading app...</div>
  //     </div>
  //   );

  return (
    <Layout>
      <ToastContainer
        position="top-right"
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
