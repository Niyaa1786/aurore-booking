using System;
using Aurore.Domain.Enums;
using Aurore.Domain.Exceptions;

namespace Aurore.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public UserRole Role { get; private set; }
        public string FullName { get; private set; } = string.Empty;
        public string? Phone { get; private set; }
        public string? RefreshToken { get; private set; }
        public DateTime? RefreshTokenExpiryTime { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private readonly List<Order> _orders = new();
        public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

        private User() { }

        public User(string fullName, string email, string passwordHash, UserRole role)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateProfile(string fullName, string? phone)
        {
            if (string.IsNullOrEmpty(fullName))
                throw new DomainException("FullName hash cannot be null or empty.");

            FullName = fullName;
            Phone = phone;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangePassword(string passwordHash)
        {
            if (string.IsNullOrEmpty(passwordHash))
                throw new DomainException("Password hash cannot be null or empty.");

            PasswordHash = passwordHash;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetRefreshToken(string token, DateTime expiry)
        {
            if (string.IsNullOrEmpty(token))
                throw new DomainException("Refresh token cannot be null or empty");

            RefreshToken = token;
            RefreshTokenExpiryTime = expiry;
            UpdatedAt = DateTime.UtcNow;
        }

        public void RevokeRefreshToken()
        {
            RefreshToken = null;
            RefreshTokenExpiryTime = null;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
