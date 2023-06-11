import { useFormik } from 'formik';
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
import * as Yup from 'yup';

interface Props {
  modal: boolean;
  toggle: () => void;
  setLoginModal: React.Dispatch<React.SetStateAction<boolean>>;
}

export default function FeedbackForm({ modal, toggle, setLoginModal }: Props) {
  const formik = useFormik({
    initialValues: {
      title: '',
      description: '',
      rating: 3,
      dateReviewed: new Date(),
      errors: null,
    },
    validationSchema: Yup.object({
      title: Yup.string().required('Title is required'),
      description: Yup.string().required('Description is required'),
      rating: Yup.number().required('Rating is required'),
    }),
    onSubmit: async (values, { setErrors }) => {
      values.dateReviewed = new Date();
      try {
        console.log(values);
        console.log(formik.errors);
      } catch (e) {
        setErrors({ errors: 'Invalid email or password' });
      }
    },
  });

  return (
    <Modal isOpen={modal} toggle={toggle}>
      <ModalHeader toggle={toggle}>Add Feedback</ModalHeader>
      <Form onSubmit={formik.handleSubmit}>
        <ModalBody>
          <FormGroup>
            <Label for="title">Title</Label>
            <Input
              id="title"
              onChange={formik.handleChange}
              value={formik.values.title}
              onBlur={formik.handleBlur}
              name="title"
              type="text"
              required
            />
            {formik.touched.title && formik.errors.title ? (
              <Alert className="mt-2 p-2" color="danger">
                {formik.errors.title}
              </Alert>
            ) : null}
          </FormGroup>
          <FormGroup>
            <Label for="description">Description</Label>
            <textarea
              id="description"
              onChange={formik.handleChange}
              value={formik.values.description}
              onBlur={formik.handleBlur}
              name="description"
              required
              className="form-control"
            />
            {formik.touched.description && formik.errors.description ? (
              <Alert className="mt-2 p-2" color="danger">
                {formik.errors.description}
              </Alert>
            ) : null}
          </FormGroup>
          <FormGroup>
            <Label for="rating">Rating</Label>
            <select
              id="rating"
              onChange={formik.handleChange}
              value={formik.values.rating}
              onBlur={formik.handleBlur}
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
            {formik.touched.rating && formik.errors.rating ? (
              <Alert className="mt-2 p-2" color="danger">
                {formik.errors.rating}
              </Alert>
            ) : null}
          </FormGroup>
        </ModalBody>
        <ModalFooter>
          <Button
            type="submit"
            disabled={formik.isSubmitting}
            color="success"
            outline
          >
            {formik.isSubmitting ? <Spinner /> : <span>Add</span>}
          </Button>{' '}
          <Button color="danger" outline onClick={toggle}>
            Cancel
          </Button>
        </ModalFooter>
      </Form>
    </Modal>
  );
}
