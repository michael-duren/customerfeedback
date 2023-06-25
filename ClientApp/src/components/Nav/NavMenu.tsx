import {useState} from 'react';
import {Link} from 'react-router-dom';
import './NavMenu.css';
import LoginForm from '../Forms/LoginForm';
import {Badge, Button, DropdownItem, DropdownMenu, DropdownToggle, UncontrolledDropdown} from 'reactstrap';
import {useStore} from '../../stores/store';
import {observer} from 'mobx-react-lite';
import {RxAvatar} from 'react-icons/rx'

export default observer(function NavMenu() {
  const {userStore} = useStore();
  const {user, logout} = userStore;
  const [loginModal, setLoginModal] = useState(false);
  const toggleLogin = () => setLoginModal(!loginModal);

  return (
    <header>
      <nav>
        <Link className="h3 logo" to={'/'}>
          Customer Feedback
        </Link>
        <div>
          <ul className="navbar-nav">
            <li>
              <Link to={'/'}>Home</Link>
            </li>
            <li>
              <Link to={'/feedback'}>Feedback</Link>
            </li>
            {/* User List for Admins */}
            {
              user?.roles.includes("Admin") &&
              <li>
                <Link to={'/admin/user-list'}>Users</Link>
              </li>
            }
            {/* Login/Logout Buttons */}
            <li>
              {!user && (
                <Button onClick={toggleLogin} color="primary">
                  Login
                </Button>
              )}{
              user?.roles.includes("Admin") && (
                <UncontrolledDropdown group>
                  <Button className="" color="primary">
                    <Link className="text-light d-flex gap-2 align-items-center text-decoration-none" to={'/'}>
                      <RxAvatar size={25}/>
                      <span>{user?.userName[0].toUpperCase()}</span>
                    </Link>
                  </Button>
                  <DropdownToggle
                    caret
                    color="primary"
                  />
                  <DropdownMenu>
                    <DropdownItem className="d-flex flex-column gap-2" header>
                      <div className={"d-flex gap-2 align-items-center"}>
                        <span><RxAvatar size={20}/></span>
                        {user?.userName}
                      </div>
                      <Badge>ADMIN</Badge>
                    </DropdownItem>
                    <DropdownItem divider/>
                    <DropdownItem text>
                    </DropdownItem>
                    <DropdownItem>
                      <Link to={'/admin/user-list'}>
                        View Users
                      </Link>
                    </DropdownItem>
                    <DropdownItem>
                      <Link to={'/feedback'}>
                        Delete Feedback
                      </Link>
                    </DropdownItem>
                    <DropdownItem divider/>
                    <DropdownItem onClick={() => logout()}>
                      Logout
                    </DropdownItem>
                  </DropdownMenu>
                </UncontrolledDropdown>
              )} {user && !(user.roles.includes("Admin")) && (
              <Button onClick={() => logout()} className="btn-dark">
                Logout
              </Button>
            )}
            </li>
          </ul>
        </div>
      </nav>
      <LoginForm
        toggle={toggleLogin}
        setLoginModal={setLoginModal}
        modal={loginModal}
      />
    </header>
  )
    ;
});
