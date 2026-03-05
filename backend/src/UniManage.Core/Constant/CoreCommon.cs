namespace UniManage.Core.Constant
{
    using System.Collections.Generic;

    public static class CoreCommon
    {
        public static class Type
        {
            /// <summary>
            /// Trạng thái
            /// </summary>
            public const string Commonstatus = "CommonStatus";

            /// <summary>
            /// Trạng thái đơn hàng
            /// </summary>
            public const string Invoicestatus = "InvoiceStatus";

            /// <summary>
            /// Ngôn ngữ
            /// </summary>
            public const string Language = "Language";

            /// <summary>
            /// Trạng thái đơn
            /// </summary>
            public const string Requeststatus = "RequestStatus";

            /// <summary>
            /// Loại đơn
            /// </summary>
            public const string Requesttype = "RequestType";

            /// <summary>
            /// Giới tính
            /// </summary>
            public const string Sex = "Sex";

        }

        public static class Value
        {
            public static class Commonstatus
            {
                /// <summary>
                /// Kích hoạt
                /// </summary>
                public const string Active = "active";

                /// <summary>
                /// Vô hiệu
                /// </summary>
                public const string Inactive = "inactive";

                /// <summary>
                /// Danh sách các giá trị đang hoạt động (Status = 1)
                /// </summary>
                public static readonly List<string> All = new List<string> { Active, Inactive };

                /// <summary>
                /// Danh sách tất cả các giá trị (bao gồm cả Inactive)
                /// </summary>
                public static readonly List<string> AllWithInactive = new List<string> { Active, Inactive };
            }

            public static class Invoicestatus
            {
                /// <summary>
                /// Mới
                /// </summary>
                public const string New = "new";

                /// <summary>
                /// Đang xử lý
                /// </summary>
                public const string Processing = "processing";

                /// <summary>
                /// Giao hàng
                /// </summary>
                public const string Delivery = "delivery";

                /// <summary>
                /// Hoàn thành
                /// </summary>
                public const string Complete = "complete";

                /// <summary>
                /// Huỷ đơn
                /// </summary>
                public const string Cancel = "cancel";

                /// <summary>
                /// Trả hàng
                /// </summary>
                public const string Refund = "refund";

                /// <summary>
                /// Danh sách các giá trị đang hoạt động (Status = 1)
                /// </summary>
                public static readonly List<string> All = new List<string> { New, Processing, Delivery, Complete, Cancel, Refund };

                /// <summary>
                /// Danh sách tất cả các giá trị (bao gồm cả Inactive)
                /// </summary>
                public static readonly List<string> AllWithInactive = new List<string> { New, Processing, Delivery, Complete, Cancel, Refund };
            }

            public static class Language
            {
                /// <summary>
                /// English
                /// </summary>
                public const string English = "english";

                /// <summary>
                /// Tiếng Việt
                /// </summary>
                public const string Vietnamese = "vietnamese";

                /// <summary>
                /// 中文
                /// </summary>
                public const string Zhongwen = "zhongwen";

                /// <summary>
                /// Danh sách các giá trị đang hoạt động (Status = 1)
                /// </summary>
                public static readonly List<string> All = new List<string> { English, Vietnamese, Zhongwen };

                /// <summary>
                /// Danh sách tất cả các giá trị (bao gồm cả Inactive)
                /// </summary>
                public static readonly List<string> AllWithInactive = new List<string> { English, Vietnamese, Zhongwen };
            }

            public static class Requeststatus
            {
                /// <summary>
                /// Chưa gửi
                /// </summary>
                public const string Draft = "draft";

                /// <summary>
                /// Đã gửi
                /// </summary>
                public const string Submitted = "submitted";

                /// <summary>
                /// Đã duyệt
                /// </summary>
                public const string Approved = "approved";

                /// <summary>
                /// Đã từ chối
                /// </summary>
                public const string Rejected = "rejected";

                /// <summary>
                /// Đã hủy
                /// </summary>
                public const string Cancelled = "cancelled";

                /// <summary>
                /// Danh sách các giá trị đang hoạt động (Status = 1)
                /// </summary>
                public static readonly List<string> All = new List<string> { Draft, Submitted, Approved, Rejected, Cancelled };

                /// <summary>
                /// Danh sách tất cả các giá trị (bao gồm cả Inactive)
                /// </summary>
                public static readonly List<string> AllWithInactive = new List<string> { Draft, Submitted, Approved, Rejected, Cancelled };
            }

            public static class Requesttype
            {
                /// <summary>
                /// Đăng ký nghỉ phép
                /// </summary>
                public const string Leave = "Leave";

                /// <summary>
                /// Đăng ký tăng ca
                /// </summary>
                public const string Overtime = "Overtime";

                /// <summary>
                /// Đăng ký công tác
                /// </summary>
                public const string Businesstrip = "BusinessTrip";

                /// <summary>
                /// Danh sách các giá trị đang hoạt động (Status = 1)
                /// </summary>
                public static readonly List<string> All = new List<string> { Leave, Overtime, Businesstrip };

                /// <summary>
                /// Danh sách tất cả các giá trị (bao gồm cả Inactive)
                /// </summary>
                public static readonly List<string> AllWithInactive = new List<string> { Leave, Overtime, Businesstrip };
            }

            public static class Sex
            {
                /// <summary>
                /// Nữ
                /// </summary>
                public const string Female = "female";

                /// <summary>
                /// Nam
                /// </summary>
                public const string Male = "male";

                /// <summary>
                /// Danh sách các giá trị đang hoạt động (Status = 1)
                /// </summary>
                public static readonly List<string> All = new List<string> { Female, Male };

                /// <summary>
                /// Danh sách tất cả các giá trị (bao gồm cả Inactive)
                /// </summary>
                public static readonly List<string> AllWithInactive = new List<string> { Female, Male };
            }

        }
    }
}
