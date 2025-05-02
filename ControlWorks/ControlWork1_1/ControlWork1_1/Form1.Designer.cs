// MIT License
// 
// Copyright (c) 2025 AleksandrVoskresenskii
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction...
// 
// Full license text is available in the LICENSE file.

namespace ControlWork1_1;

/// <summary>
/// Частичный класс, созданный дизайнером.
/// Содержит метод <see cref="InitializeComponent"/> для
/// развёртывания элементов управления.
/// </summary>
public partial class Form1
{
    /// <summary>
    /// Кнопка, убегающая от курсора и завершающая приложение по клику.
    /// </summary>
    private Button runawayButton = null!;

    /// <summary>
    /// Метод, вызываемый из конструктора для создания
    /// и размещения контролов.
    /// </summary>
    private void InitializeComponent()
    {
        this.runawayButton = new Button();
        this.SuspendLayout();

        this.runawayButton.AutoSize = true;
        this.runawayButton.Location = new Point(100, 100);
        this.runawayButton.Name = "runawayButton";
        this.runawayButton.Size = new Size(120, 30);
        this.runawayButton.TabIndex = 0;
        this.runawayButton.Text = "Поймай меня!";
        this.runawayButton.UseVisualStyleBackColor = true;
 
        this.AutoScaleDimensions = new SizeF(8F, 20F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(600, 400);
        this.Controls.Add(this.runawayButton);
        this.Name = "Form1";
        this.Text = "Контрольная 1.1 — Убегающая кнопка";
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
