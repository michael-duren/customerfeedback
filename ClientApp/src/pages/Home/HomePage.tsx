import { useState } from 'react';
import {
  Button,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
  Form,
  FormGroup,
  Label,
  Input,
} from 'reactstrap';
import './HomePage.styles.css';

export default function HomePage() {
  const [loginModal, setLoginModal] = useState(false);

  const toggle = () => setLoginModal(!loginModal);

  return (
    <div className="container">
      <main>
        <div className="main">
          <h1 className="h2">Welcome.</h1>
          <h2 className="h3">Sign in to get started or register</h2>
          <div className="auth-buttons">
            <button onClick={toggle} className="btn btn-lg btn-primary">
              Login
            </button>
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
        {/* modals */}
        {/* login */}
        <div>
          <Modal isOpen={loginModal} toggle={toggle}>
            <ModalHeader toggle={toggle}>Modal title</ModalHeader>
            <ModalBody>
              <Form>
                <FormGroup>
                  <Label for="exampleEmail">Email</Label>
                  <Input id="exampleEmail" name="email" type="email" />
                </FormGroup>
                <FormGroup>
                  <Label for="examplePassword">Password</Label>
                  <Input id="examplePassword" name="password" type="password" />
                </FormGroup>
              </Form>
            </ModalBody>
            <ModalFooter>
              <Button color="success" outline onClick={toggle}>
                Login
              </Button>{' '}
              <Button color="danger" outline onClick={toggle}>
                Cancel
              </Button>
            </ModalFooter>
          </Modal>
        </div>
      </main>
    </div>
  );
}
