import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';

interface Props {
  title: string;
  action?: string;
  modal: boolean;
  toggle: () => void;
  children?: React.ReactNode;
}

export default function GenericModal({
  title,
  action,
  modal,
  toggle,
  children,
}: Props) {
  return (
    <Modal isOpen={modal} toggle={toggle}>
      <ModalHeader toggle={toggle}>{title}</ModalHeader>
      <ModalBody>{children}</ModalBody>
      <ModalFooter>
        {action && (
          <Button color="success" outline onClick={toggle}>
            {action}
          </Button>
        )}
        <Button color="danger" outline onClick={toggle}>
          Cancel
        </Button>
      </ModalFooter>
    </Modal>
  );
}
