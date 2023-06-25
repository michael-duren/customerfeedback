import {FaSearch} from 'react-icons/fa';
import {Link} from 'react-router-dom';
import './Errors.styles.css';

export default function NotFound() {
  return (
    <main className="d-flex align-items-center justify-content-center mt-xxl-5 flex-column">
      <div className="d-flex gap-4">
        <FaSearch size={40}/>
        <h2 className="">Oops, we can't find what you're looking for...</h2>
      </div>
      <div className="mt-8">
        <Link className="returnLink" to={'/'}>
          <button className="btn-lg btn btn-success" type="submit">
            Return
          </button>
        </Link>
      </div>
    </main>
  );
}
