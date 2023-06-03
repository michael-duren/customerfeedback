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
} from 'reactstrap';

interface Props {
  modal: boolean;
  toggle: () => void;
}

export default function RegisterForm({ modal, toggle }: Props) {
  return (
    <Modal isOpen={modal} toggle={toggle}>
      <ModalHeader toggle={toggle}>Welcome! Register Below!</ModalHeader>
      <Form>
        <ModalBody>
          <FormGroup>
            <Label for="name">Name</Label>
            <Input id="name" name="name" type="text" />
          </FormGroup>
          <FormGroup>
            <Label for="email">Email</Label>
            <Input id="email" name="email" type="email" />
          </FormGroup>
          <FormGroup>
            <Label for="password">Password</Label>
            <Input id="password" name="password" type="password" />
          </FormGroup>
          <FormGroup>
            <Label for="passwordVerified">Re-enter Password</Label>
            <Input
              id="passwordVerified"
              name="passwordVerified"
              type="password"
            />
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
