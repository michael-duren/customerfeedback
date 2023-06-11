export interface Feedback {
  id: number;
  title: string;
  description: string;
  rating: number;
  dateReviewed: string;
}

export interface FeedbackFormValues {
  title: string;
  description: string;
  rating: number;
  dateReviewed: string;
}
