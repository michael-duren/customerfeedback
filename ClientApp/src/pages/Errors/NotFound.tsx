import { FaSearch } from 'react-icons/fa';
import { Link } from 'react-router-dom';
import { Button } from 'reactstrap';
import './Errors.styles.css';

export default function NotFound() {
  return (
    <main className="">
      <div className="">
        <FaSearch size={40} />
        <h2 className="">Oops, we can't find what you're looking for...</h2>
      </div>
      <div className="mt-8">
        <Button color="primary" type="submit">
          <Link className="returnLink" to={'/'}>
            Return
          </Link>
        </Button>
      </div>
    </main>
  );
}
