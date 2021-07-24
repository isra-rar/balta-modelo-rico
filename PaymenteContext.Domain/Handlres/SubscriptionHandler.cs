using System;
using Flunt.Notifications;
using PaymenteContext.Domain.Commands;
using PaymenteContext.Domain.Entities;
using PaymenteContext.Domain.Enums;
using PaymenteContext.Domain.Repositories;
using PaymenteContext.Domain.Services;
using PaymenteContext.Domain.ValueObjects;
using PaymenteContext.Shared.Commands;
using PaymenteContext.Shared.Handlers;

namespace PaymenteContext.Domain.Handlres
{
    public class SubscriptionHandler :
    Notifiable<Notification>, 
    IHandler<CreateBoletoSubscriptionCommand>,
    IHandler<CreatePayPalSubscriptionCommand>,
    IHandler<CreateCreditCardSubscriptionCommand>
    {

        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository studentRepository, IEmailService emailService)
        {
            _studentRepository = studentRepository;
            _emailService = emailService;
        }
        public ICommandResult Handler(CreateBoletoSubscriptionCommand command)
        {
            // Fail Fast Validation
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel realizar sua assinatura");
            }

            // Verificar se documento já está cadastrado
            if (_studentRepository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            // Verificar se Email já está cadastrado
            if (_studentRepository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            // Gerar os VOS

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Document);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State,
            command.Country, command.ZipCode);

            // Gerar as Entidades
            var student = new Student(name, document, email, address);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.BarCode, command.BoletoNumber, command.PaidDate, 
            command.ExpireDate, command.Total, command.TotalPaid, command.Payer, 
            new Document(command.PayerDocument, command.PayerDocumentType), address, email);

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Checar as notificaoes
            if (!IsValid)
                return new CommandResult(false, "Não foi possivel realizar sua assinatura");

            // Salvar as informações
            _studentRepository.CreateSubscription(student);

            // Enviar e-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, 
            "Bem vindo ao balta.io", "Sua Assinatura foi criada");

            // Retornar informações
            return new CommandResult(true, "Assinatura realizada com Sucesso");
        }

        public ICommandResult Handler(CreatePayPalSubscriptionCommand command)
        {
            // Fail Fast Validation
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel realizar sua assinatura");
            }

            // Verificar se documento já está cadastrado
            if (_studentRepository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            // Verificar se Email já está cadastrado
            if (_studentRepository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            // Gerar os VOS

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Document);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State,
            command.Country, command.ZipCode);

            // Gerar as Entidades
            var student = new Student(name, document, email, address);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(command.TransactionCode, command.PaidDate, 
            command.ExpireDate, command.Total, command.TotalPaid, command.Payer, 
            new Document(command.PayerDocument, command.PayerDocumentType), address, email);

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Salvar as informações
            _studentRepository.CreateSubscription(student);

            // Enviar e-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, 
            "Bem vindo ao balta.io", "Sua Assinatura foi criada");

            // Retornar informações
            return new CommandResult(true, "Assinatura realizada com Sucesso");
        }

        public ICommandResult Handler(CreateCreditCardSubscriptionCommand command)
        {
            // Fail Fast Validation
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel realizar sua assinatura");
            }

            // Verificar se documento já está cadastrado
            if (_studentRepository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            // Verificar se Email já está cadastrado
            if (_studentRepository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            // Gerar os VOS

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Document);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State,
            command.Country, command.ZipCode);

            // Gerar as Entidades
            var student = new Student(name, document, email, address);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new CreditCardPayment(command.CardHolderName, command.CardNumber, command.LastTransactionNumber, command.PaidDate, 
            command.ExpireDate, command.Total, command.TotalPaid, command.Payer, 
            new Document(command.PayerDocument, command.PayerDocumentType), address, email);

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Salvar as informações
            _studentRepository.CreateSubscription(student);

            // Enviar e-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, 
            "Bem vindo ao balta.io", "Sua Assinatura foi criada");

            // Retornar informações
            return new CommandResult(true, "Assinatura realizada com Sucesso");
        }
    }
}