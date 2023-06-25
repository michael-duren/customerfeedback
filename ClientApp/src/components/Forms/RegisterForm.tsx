import { useFormik } from 'formik';
import {
  Form,
  FormGroup,
  Label,
  Input,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
  Button,
  Alert,
} from 'reactstrap';
import { useStore } from '../../stores/store';
import ValidationError from '../Errors/ValidationError';
import { registerFormSchema } from './schemas/registerFormSchema';

interface Props {
  modal: boolean;
  toggle: () => void;
}

export default function RegisterForm({ modal, toggle }: Props) {
  const { userStore } = useStore();
  const { register } = userStore;

  const formik = useFormik({
    initialValues: {
      displayName: '',
      userName: '',
      email: '',
      password: '',
      reEnterPassword: '',
      errors: null,
    },
    validationSchema: registerFormSchema,
    onSubmit: async (values) => {
      const { displayName, userName, email, password } = values;
      try {
        await register({ displayName, userName, email, password });
      } catch (e: any) {
        formik.setErrors({ errors: e });
      }
      register({ displayName, userName, email, password });
      console.log(values);
    },
  });

  return (
    <Modal isOpen={modal} toggle={toggle}>
      <ModalHeader toggle={toggle}>Welcome! Register Below!</ModalHeader>
      <Form onSubmit={formik.handleSubmit}>
        <ModalBody>
          <FormGroup>
            <Label for="name">Display Name</Label>
            <Input
              id="displayName"
              onChange={formik.handleChange}
              value={formik.values.displayName}
              onBlur={formik.handleBlur}
              required
              name="displayName"
              type="text"
            />
            {formik.touched.displayName && formik.errors.displayName ? (
              <Alert className="mt-2 p-2" color="danger">
                {formik.errors.displayName}
              </Alert>
            ) : null}
          </FormGroup>
          <FormGroup>
            <Label for="userName">Username</Label>
            <Input
              id="userName"
              onChange={formik.handleChange}
              value={formik.values.userName}
              onBlur={formik.handleBlur}
              required
              name="userName"
              type="text"
            />
            {formik.touched.userName && formik.errors.userName ? (
              <Alert className="mt-2 p-2" color="danger">
                {formik.errors.userName}
              </Alert>
            ) : null}
          </FormGroup>
          <FormGroup>
            <Label for="email">Email</Label>
            <Input
              id="email"
              onChange={formik.handleChange}
              value={formik.values.email}
              onBlur={formik.handleBlur}
              required
              name="email"
              type="email"
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
              onBlur={formik.handleBlur}
              required
              name="password"
              type="password"
            />
            {formik.touched.password && formik.errors.password ? (
              <Alert className="mt-2 p-2" color="danger">
                {formik.errors.password}
              </Alert>
            ) : null}
          </FormGroup>
          <FormGroup>
            <Label for="reEnterPassword">Re-enter Password</Label>
            <Input
              id="reEnterPassword"
              onChange={formik.handleChange}
              value={formik.values.reEnterPassword}
              onBlur={formik.handleBlur}
              required
              name="reEnterPassword"
              type="password"
            />

            {formik.touched.password &&
            formik.touched.reEnterPassword &&
            formik.errors.reEnterPassword ? (
              <Alert className="mt-2 p-2" color="danger">
                {formik.errors.reEnterPassword}
              </Alert>
            ) : null}
            <ValidationError errors={formik.errors.errors} />
          </FormGroup>
        </ModalBody>
        <ModalFooter>
          <Button
            type="submit"
            color="success"
            disabled={formik.isSubmitting || !formik.isValid}
            outline
          >
            Login
          </Button>{' '}
          <Button color="danger" outline onClick={toggle}>
            Cancel
          </Button>
        </ModalFooter>
      </Form>
    </Modal>
  );
}
