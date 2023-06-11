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
} from 'reactstrap';
import { Formik } from 'formik';

interface Props {
  modal: boolean;
  toggle: () => void;
}

export default function LoginForm({ modal, toggle }: Props) {
  return (
    <Modal isOpen={modal} toggle={toggle}>
      <ModalHeader toggle={toggle}>Welcome Back! Login Below!</ModalHeader>
      <Formik
        initialValues={{ email: '', password: '' }}
        onSubmit={(values) => console.log(values)}
      >
        {({ values, handleChange, handleSubmit, isSubmitting }) => (
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
                />
              </FormGroup>
            </ModalBody>
            <ModalFooter>
              <Button
                type="submit"
                disabled={isSubmitting}
                color="success"
                outline
                onClick={toggle}
              >
                Login
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
}
