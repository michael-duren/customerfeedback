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

interface Props {
  modal: boolean;
  toggle: () => void;
}

export default function LoginForm({ modal, toggle }: Props) {
  return (
    <Modal isOpen={modal} toggle={toggle}>
      <ModalHeader toggle={toggle}>Welcome Back! Login Below!</ModalHeader>
      <Form>
        <ModalBody>
          <FormGroup>
            <Label for="exampleEmail">Email</Label>
            <Input id="exampleEmail" name="email" type="email" />
          </FormGroup>
          <FormGroup>
            <Label for="examplePassword">Password</Label>
            <Input id="examplePassword" name="password" type="password" />
          </FormGroup>
        </ModalBody>
        <ModalFooter>
          <Button type="submit" color="success" outline onClick={toggle}>
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
