import HomePage from './pages/Home/HomePage.tsx';
import FeedbackPage from './pages/Feedback/FeedBackPage.tsx';

const AppRoutes = [
  {
    index: true,
    element: <HomePage />,
  },
  {
    path: '/feedback',
    element: <FeedbackPage />,
  },
];

export default AppRoutes;
