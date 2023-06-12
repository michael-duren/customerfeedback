import * as Yup from 'yup';

export const feedbackFormSchema = Yup.object({
  title: Yup.string().required().min(5),
  description: Yup.string().required().min(10),
  rating: Yup.number().required('Rating is required'),
});
