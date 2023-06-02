import { Counter } from './components/Counter';
import { FetchData } from './components/FetchData';
import HomePage from './pages/HomePage.tsx';

const AppRoutes = [
  {
    index: true,
    element: <HomePage />,
  },
  {
    path: '/feedback',
    element: <Counter />,
  },
  {
    path: '/',
    element: <FetchData />,
  },
];

export default AppRoutes;
