import { Formik } from 'formik';
import {
  Alert,
  Button,
  Form,
  FormGroup,
  Input,
  Label,
  Modal,
  ModalBody,
  ModalFooter,
  ModalHeader,
  Spinner,
} from 'reactstrap';

interface Props {
  modal: boolean;
  toggle: () => void;
  setLoginModal: React.Dispatch<React.SetStateAction<boolean>>;
}

export default function FeedbackForm({ modal, toggle, setLoginModal }: Props) {
  return (
    <Modal isOpen={modal} toggle={toggle}>
      <ModalHeader toggle={toggle}>Add Feedback</ModalHeader>
      <Formik
        initialValues={{
          title: '',
          description: '',
          rating: 3,
          dateReviewed: new Date(),
          errors: null,
        }}
        onSubmit={async (values, { setErrors }) => {
          values.dateReviewed = new Date();
          try {
            console.log(values);
          } catch (e) {
            setErrors({ errors: 'Invalid email or password' });
          }
        }}
      >
        {({ values, errors, handleChange, handleSubmit, isSubmitting }) => (
          <Form onSubmit={handleSubmit}>
            <ModalBody>
              <FormGroup>
                <Label for="title">Title</Label>
                <Input
                  onChange={handleChange}
                  value={values.title}
                  id="title"
                  name="title"
                  type="text"
                  required
                />
              </FormGroup>
              <FormGroup>
                <Label for="description">Description</Label>
                <textarea
                  id="description"
                  onChange={handleChange}
                  value={values.description}
                  name="description"
                  className="form-control"
                  required
                />
              </FormGroup>
              <FormGroup>
                <Label for="rating">Rating</Label>
                <select
                  id="rating"
                  onChange={handleChange}
                  value={values.rating}
                  name="rating"
                  className="form-control"
                  required
                >
                  <option value="1">1</option>
                  <option value="2">2</option>
                  <option value="3">3</option>
                  <option value="4">4</option>
                  <option value="5">5</option>
                </select>
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
                {isSubmitting ? <Spinner /> : <span>Add</span>}
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
