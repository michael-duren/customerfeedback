import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';

interface Props {
  modal: boolean;
  toggle: () => void;
  children?: React.ReactNode;
}

export default function GenericModal({ modal, toggle, children }: Props) {
  return (
    <Modal isOpen={modal} toggle={toggle}>
      <ModalHeader toggle={toggle}>Modal title</ModalHeader>
      <ModalBody>{children}</ModalBody>
      <ModalFooter>
        <Button color="success" outline onClick={toggle}>
          Login
        </Button>{' '}
        <Button color="danger" outline onClick={toggle}>
          Cancel
        </Button>
      </ModalFooter>
    </Modal>
  );
}
