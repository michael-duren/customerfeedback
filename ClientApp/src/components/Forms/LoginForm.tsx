import {
  Form,
  FormGroup,
  Label,
  Input,
  ModalBody,
  ModalFooter,
  Button,
  ModalHeader,
  Modal,
  Spinner,
  Alert,
} from 'reactstrap';
import { Formik } from 'formik';
import { useStore } from '../../stores/store';
import { useEffect } from 'react';
import { observer } from 'mobx-react-lite';

interface Props {
  modal: boolean;
  toggle: () => void;
  setLoginModal: React.Dispatch<React.SetStateAction<boolean>>;
}

export default observer(function LoginForm({
  modal,
  toggle,
  setLoginModal,
}: Props) {
  const { userStore } = useStore();
  const { login, user } = userStore;

  useEffect(() => {
    if (user) {
      setLoginModal(false);
    }
  }, [user]);

  return (
    <Modal isOpen={modal} toggle={toggle}>
      <ModalHeader toggle={toggle}>Welcome Back! Login Below!</ModalHeader>
      <Formik
        initialValues={{ email: '', password: '', errors: null }}
        onSubmit={async (values, { setErrors }) => {
          try {
            await login(values);
          } catch (e) {
            setErrors({ errors: 'Invalid email or password' });
          }
        }}
      >
        {({ values, errors, handleChange, handleSubmit, isSubmitting }) => (
          <Form onSubmit={handleSubmit}>
            <ModalBody>
              <FormGroup>
                <Label for="email">Email</Label>
                <Input
                  onChange={handleChange}
                  value={values.email}
                  id="email"
                  name="email"
                  type="email"
                  required
                />
              </FormGroup>
              <FormGroup>
                <Label for="password">Password</Label>
                <Input
                  id="password"
                  onChange={handleChange}
                  value={values.password}
                  name="password"
                  type="password"
                  required
                />
                {errors.errors && (
                  <Alert className="mt-2 p-2" color="danger">
                    {errors.errors}
                  </Alert>
                )}
              </FormGroup>
            </ModalBody>
            <ModalFooter>
              <Button
                type="submit"
                disabled={isSubmitting}
                color="success"
                outline
              >
                {isSubmitting ? <Spinner /> : <span>Login</span>}
              </Button>{' '}
              <Button color="danger" outline onClick={toggle}>
                Cancel
              </Button>
            </ModalFooter>
          </Form>
        )}
      </Formik>
    </Modal>
  );
});
