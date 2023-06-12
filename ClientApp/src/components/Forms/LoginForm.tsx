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
import { useFormik } from 'formik';
import { useStore } from '../../stores/store';
import { useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { loginFormSchema } from './schemas/loginFormSchema';

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

  const formik = useFormik({
    initialValues: { email: '', password: '', errors: null },
    validationSchema: loginFormSchema,
    onSubmit: async (values) => {
      try {
        await login(values);
      } catch (e) {
        formik.setErrors({ errors: 'Invalid email or password' });
      }
    },
  });

  return (
    <Modal isOpen={modal} toggle={toggle}>
      <ModalHeader toggle={toggle}>Welcome Back! Login Below!</ModalHeader>
      <Form onSubmit={formik.handleSubmit}>
        <ModalBody>
          <FormGroup>
            <Label for="email">Email</Label>
            <Input
              onChange={formik.handleChange}
              value={formik.values.email}
              id="email"
              name="email"
              type="email"
              required
            />
            {formik.touched.email && formik.errors.email ? (
              <Alert className="mt-2 p-2" color="danger">
                {formik.errors.email}
              </Alert>
            ) : null}
          </FormGroup>
          <FormGroup>
            <Label for="password">Password</Label>
            <Input
              id="password"
              onChange={formik.handleChange}
              value={formik.values.password}
              name="password"
              type="password"
              required
            />
            {formik.touched.password && formik.errors.password ? (
              <Alert className="mt-2 p-2" color="danger">
                {formik.errors.password}
              </Alert>
            ) : null}
            {formik.errors.errors && (
              <Alert className="mt-2 p-2" color="danger">
                {formik.errors.errors}
              </Alert>
            )}
          </FormGroup>
        </ModalBody>
        <ModalFooter>
          <Button
            type="submit"
            disabled={formik.isSubmitting}
            color="success"
            outline
          >
            {formik.isSubmitting ? <Spinner size={'sm'} /> : <span>Login</span>}
          </Button>{' '}
          <Button color="danger" outline onClick={toggle}>
            Cancel
          </Button>
        </ModalFooter>
      </Form>
    </Modal>
  );
});
