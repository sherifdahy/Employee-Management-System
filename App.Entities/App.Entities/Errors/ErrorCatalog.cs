using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities
{
    public static class ErrorCatalog
    {
        public static class Auth
        {
            public static readonly ErrorInfo InvalidCredentials = new("AUTH_001", "البريد الإلكتروني أو كلمة المرور غير صحيحة.");
            public static readonly ErrorInfo UserNotFound = new("AUTH_002", "المستخدم غير موجود.");
            public static readonly ErrorInfo Unauthorized = new("AUTH_003", "غير مصرح لك بالوصول.");
            public static readonly ErrorInfo AccountLocked = new("AUTH_004", "تم قفل الحساب مؤقتاً بسبب محاولات فاشلة متكررة.");
            public static readonly ErrorInfo AccountNotActivated = new("AUTH_005", "الحساب غير مفعل، يرجى تفعيل الحساب.");
        }

        public static class Registration
        {
            public static readonly ErrorInfo EmailAlreadyExists = new("REG_001", "هذا البريد الإلكتروني مستخدم مسبقاً.");
            public static readonly ErrorInfo UsernameAlreadyExists = new("REG_002", "اسم المستخدم مستخدم مسبقاً.");
            public static readonly ErrorInfo WeakPassword = new("REG_003", "كلمة المرور ضعيفة، يرجى اختيار كلمة أقوى.");
        }

        public static class Validation
        {
            public static readonly ErrorInfo RequiredFieldMissing = new("VALIDATION_001", "يوجد حقل مطلوب لم يتم إدخاله.");
            public static readonly ErrorInfo InvalidEmailFormat = new("VALIDATION_002", "صيغة البريد الإلكتروني غير صحيحة.");
            public static readonly ErrorInfo PasswordTooWeak = new("VALIDATION_003", "كلمة المرور ضعيفة.");
            public static readonly ErrorInfo InvalidPhoneNumber = new("VALIDATION_004", "رقم الهاتف غير صحيح.");
            public static readonly ErrorInfo FieldLengthExceeded = new("VALIDATION_005", "تم تجاوز الحد الأقصى لطول الحقل.");
            public static readonly ErrorInfo FieldsDoNotMatch = new("VALIDATION_006", "الحقلان غير متطابقين.");
        }

        public static class Database
        {
            public static readonly ErrorInfo UniqueConstraintViolation = new("DB_001", "هذا السجل موجود مسبقًا.");
            public static readonly ErrorInfo ForeignKeyViolation = new("DB_002", "لا يمكن حذف هذا السجل لارتباطه ببيانات أخرى.");
            public static readonly ErrorInfo RecordNotFound = new("DB_003", "السجل المطلوب غير موجود.");
            public static readonly ErrorInfo ConcurrencyViolation = new("DB_004", "تم تعديل السجل من قِبل مستخدم آخر.");
        }

        public static class Server
        {
            public static readonly ErrorInfo Unexpected = new("SERVER_001", "حدث خطأ غير متوقع. برجاء المحاولة لاحقًا.");
            public static readonly ErrorInfo Timeout = new("SERVER_002", "انتهت مهلة التنفيذ.");
            public static readonly ErrorInfo ServiceUnavailable = new("SERVER_003", "الخدمة غير متاحة حالياً.");
        }

        public static class Token
        {
            public static readonly ErrorInfo InvalidToken = new("TOKEN_001", "رمز التحقق غير صالح.");
            public static readonly ErrorInfo TokenExpired = new("TOKEN_002", "انتهت صلاحية الرمز.");
            public static readonly ErrorInfo TokenRevoked = new("TOKEN_003", "تم إلغاء صلاحية الرمز.");
        }

        public static class Access
        {
            public static readonly ErrorInfo Forbidden = new("ACCESS_001", "ليس لديك صلاحيات للقيام بهذا الإجراء.");
            public static readonly ErrorInfo ResourceLocked = new("ACCESS_002", "المورد المطلوب مقفل حاليًا.");
        }

        public static class BusinessRules
        {
            public static readonly ErrorInfo OperationNotAllowed = new("BUSINESS_001", "لا يمكن تنفيذ هذه العملية حالياً.");
            public static readonly ErrorInfo MaxLimitReached = new("BUSINESS_002", "تم الوصول إلى الحد الأقصى المسموح.");
            public static readonly ErrorInfo InvalidOperationState = new("BUSINESS_003", "الحالة الحالية لا تسمح بتنفيذ هذا الإجراء.");
        }


        public static class FilesRules
        {
            public static readonly ErrorInfo FileNotExist = new("FILES_001","المسار الذي ادخلتة غير صحيح");
            public static readonly ErrorInfo FileIsEmpty = new("FILES_002","الملف فارغ");
        }
    }

}
